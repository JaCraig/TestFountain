using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
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
    public class FountainDataAttribute : DataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FountainDataAttribute"/> class.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <param name="maxDuration">The duration in ms. (overrides the count)</param>
        public FountainDataAttribute(int count, int maxDuration = int.MaxValue)
        {
            Manager = Canister.Builder.Bootstrapper?.Resolve<GeneratorManager>();
            var DataSources = Canister.Builder.Bootstrapper?.ResolveAll<IDatasource>();

            DataSource = DataSources.FirstOrDefault(x => x.GetType().Name.IndexOf("TESTFOUNTAIN", StringComparison.OrdinalIgnoreCase) < 0)
                ?? DataSources.FirstOrDefault(x => x.GetType().Name.IndexOf("TESTFOUNTAIN", StringComparison.OrdinalIgnoreCase) >= 0);
            Count = count;
            MaxDuration = maxDuration;
            Finished = false;
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
        private IDatasource DataSource { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FountainDataAttribute"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        private bool Finished { get; set; }

        /// <summary>
        /// Gets or sets the generator.
        /// </summary>
        /// <value>The generator.</value>
        private GeneratorManager Manager { get; set; }

        /// <summary>
        /// Returns the data to be used to test the theory.
        /// </summary>
        /// <param name="testMethod">The method that is being tested</param>
        /// <returns>
        /// One or more sets of theory data. Each invocation of the test method is represented by a
        /// single object array.
        /// </returns>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (Manager == null)
                throw new ArgumentNullException(nameof(Manager));

            var Parameters = testMethod.GetParameters();
            var Data = new object[Parameters.Length];
            Finished = false;
            using (var InternalTimer = new Timer(MaxDuration))
            {
                InternalTimer.Elapsed += InternalTimer_Elapsed;
                InternalTimer?.Start();

                var PreviousData = DataSource.Read(testMethod);
                var PreviousDataCount = Count <= PreviousData.Count ? Count : PreviousData.Count;
                for (int x = 0; x < PreviousDataCount; ++x)
                {
                    yield return PreviousData[x];
                }

                for (int x = PreviousDataCount; x < Count; ++x)
                {
                    Data = Manager.Next(Parameters);
                    DataSource.Save(testMethod, Data);
                    yield return Data;
                    if (Finished)
                        break;
                }
                InternalTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the InternalTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void InternalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Finished = true;
        }
    }
}