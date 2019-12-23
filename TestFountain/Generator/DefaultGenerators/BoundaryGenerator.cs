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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator.DefaultGenerators
{
    /// <summary>
    /// Boundary generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class BoundaryGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundaryGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public BoundaryGenerator(Mirage.Random random)
        {
            Random = random;
            SetupMaxValues();
            SetupMinValues();
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        private Mirage.Random Random { get; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        private Dictionary<Type, object>? Max { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        private Dictionary<Type, object>? Min { get; set; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue
                && parameter.GetCustomAttribute<ValidationAttribute>() == null
                && (Max?.ContainsKey(parameter.ParameterType) ?? false);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object Next(ParameterInfo parameter)
        {
            return Random.Next<bool>() ? (Max?[parameter.ParameterType] ?? false) : (Min?[parameter.ParameterType] ?? false);
        }

        /// <summary>
        /// Setups the maximum values.
        /// </summary>
        private void SetupMaxValues()
        {
            Max = new Dictionary<Type, object>
            {
                { typeof(int), int.MaxValue },
                { typeof(short), short.MaxValue },
                { typeof(long), long.MaxValue },
                { typeof(float), float.MaxValue },
                { typeof(double), double.MaxValue },
                { typeof(decimal), decimal.MaxValue },
                { typeof(uint), uint.MaxValue },
                { typeof(ushort), ushort.MaxValue },
                { typeof(ulong), ulong.MaxValue },
                { typeof(bool), true },
                { typeof(DateTime), DateTime.MaxValue },
                { typeof(char), char.MaxValue },
                { typeof(byte), byte.MaxValue },
                { typeof(sbyte), sbyte.MaxValue },
                { typeof(TimeSpan), TimeSpan.MaxValue },

                { typeof(int?), int.MaxValue },
                { typeof(short?), short.MaxValue },
                { typeof(long?), long.MaxValue },
                { typeof(float?), float.MaxValue },
                { typeof(double?), double.MaxValue },
                { typeof(decimal?), decimal.MaxValue },
                { typeof(uint?), uint.MaxValue },
                { typeof(ushort?), ushort.MaxValue },
                { typeof(ulong?), ulong.MaxValue },
                { typeof(bool?), true },
                { typeof(DateTime?), DateTime.MaxValue },
                { typeof(char?), char.MaxValue },
                { typeof(byte?), byte.MaxValue },
                { typeof(sbyte?), sbyte.MaxValue },
                { typeof(TimeSpan?), TimeSpan.MaxValue }
            };
        }

        /// <summary>
        /// Setups the minimum values.
        /// </summary>
        private void SetupMinValues()
        {
            Min = new Dictionary<Type, object>
            {
                { typeof(int), int.MinValue },
                { typeof(short), short.MinValue },
                { typeof(long), long.MinValue },
                { typeof(float), float.MinValue },
                { typeof(double), double.MinValue },
                { typeof(decimal), decimal.MinValue },
                { typeof(uint), uint.MinValue },
                { typeof(ushort), ushort.MinValue },
                { typeof(ulong), ulong.MinValue },
                { typeof(bool), true },
                { typeof(DateTime), DateTime.MinValue },
                { typeof(char), char.MinValue },
                { typeof(byte), byte.MinValue },
                { typeof(sbyte), sbyte.MinValue },
                { typeof(TimeSpan), TimeSpan.MinValue },

                { typeof(int?), int.MinValue },
                { typeof(short?), short.MinValue },
                { typeof(long?), long.MinValue },
                { typeof(float?), float.MinValue },
                { typeof(double?), double.MinValue },
                { typeof(decimal?), decimal.MinValue },
                { typeof(uint?), uint.MinValue },
                { typeof(ushort?), ushort.MinValue },
                { typeof(ulong?), ulong.MinValue },
                { typeof(bool?), true },
                { typeof(DateTime?), DateTime.MinValue },
                { typeof(char?), char.MinValue },
                { typeof(byte?), byte.MinValue },
                { typeof(sbyte?), sbyte.MinValue },
                { typeof(TimeSpan?), TimeSpan.MinValue }
            };
        }
    }
}