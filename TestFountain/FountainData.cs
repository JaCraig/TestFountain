/*
Copyright 2018 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using BigBook;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using TestFountain.DataSources;
using TestFountain.DataSources.Interfaces;
using TestFountain.Generator;
using Xunit.Sdk;

namespace TestFountain
{
    /// <summary>
    /// Data generator class used in theory methods.
    /// </summary>
    /// <seealso cref="DataAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class FountainDataAttribute : DataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FountainDataAttribute"/> class.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <param name="maxDuration">The duration in ms. (overrides the count)</param>
        public FountainDataAttribute(int count, int maxDuration = int.MaxValue)
        {
            ServiceProvider? Services = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider();
            if (Services is null)
                return;
            Manager = Services.GetService<GeneratorManager>();
            IEnumerable<IDatasource> DataSources = Services.GetServices<IDatasource>();

            DataSource = DataSources.FirstOrDefault(x => x is not DefaultDataSource) ?? DataSources.FirstOrDefault(x => x is DefaultDataSource);
            Count = count;
            MaxDuration = maxDuration;
            Finished = false;
            PreviousItems = [];
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public int MaxDuration { get; }

        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <value>The data source.</value>
        private IDatasource? DataSource { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FountainDataAttribute"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        private bool Finished { get; set; }

        /// <summary>
        /// Gets or sets the generator.
        /// </summary>
        /// <value>The generator.</value>
        private GeneratorManager? Manager { get; }

        /// <summary>
        /// Gets or sets the previous items.
        /// </summary>
        /// <value>The previous items.</value>
        private List<object?[]> PreviousItems { get; } = [];

        /// <summary>
        /// Returns the data to be used to test the theory.
        /// </summary>
        /// <param name="testMethod">The method that is being tested</param>
        /// <returns>
        /// One or more sets of theory data. Each invocation of the test method is represented by a
        /// single object array.
        /// </returns>
        public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
        {
            return Manager is null ? throw new NullReferenceException(nameof(Manager)) : GetData2();

            IEnumerable<object?[]> GetData2()
            {
                ParameterInfo[] Parameters = testMethod.GetParameters();
                var Data = new object?[Parameters.Length];
                Finished = false;
                using var InternalTimer = new Timer(MaxDuration);
                InternalTimer.Elapsed += InternalTimer_Elapsed;
                InternalTimer?.Start();

                List<object?[]> PreviousData = DataSource?.Read(testMethod) ?? [];
                var PreviousDataCount = Count <= PreviousData.Count ? Count : PreviousData.Count;
                for (var X = 0; X < PreviousDataCount; ++X)
                {
                    yield return PreviousData[X];
                }

                for (var X = PreviousDataCount; X < Count;)
                {
                    Data = Manager.Next(Parameters);
                    if (PreviousItems.AddIfUnique(Same, Data))
                    {
                        DataSource?.Save(testMethod, Data);
                        yield return Data;
                        ++X;
                    }
                    if (Finished)
                        break;
                }
                InternalTimer?.Stop();
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the InternalTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void InternalTimer_Elapsed(object? sender, ElapsedEventArgs e) => Finished = true;

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private bool Same(object?[] value1, object?[] value2)
        {
            if (value1 == null || value2 == null)
                return false;
            if (value1.Length != value2.Length)
                return false;
            for (var X = 0; X < value1.Length; ++X)
            {
                var Value1 = JsonConvert.SerializeObject(value1[X]);
                var Value2 = JsonConvert.SerializeObject(value2[X]);
                if (Value1 != Value2)
                    return false;
            }
            return true;
        }
    }
}