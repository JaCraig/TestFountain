using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace TestFountain.Tests.Fixtures
{
    [CollectionDefinition("Canister collection")]
    public class CanisterCollection : ICollectionFixture<CanisterFixture>
    {
    }

    public class CanisterFixture : IDisposable
    {
        public CanisterFixture()
        {
            if (Services is not null)
                return;
            Services = new ServiceCollection().AddCanisterModules(configure => configure.RegisterTestFountain().AddAssembly(typeof(CanisterFixture).Assembly)).BuildServiceProvider();
        }

        private IServiceProvider Services { get; set; }

        public void Dispose()
        {
            new DirectoryInfo("./TestFountain/SavedTests/TestFountain.Tests.Generator/").Delete();
            new DirectoryInfo("./TestFountain/SavedTests/TestFountain.Tests.DataSources/").Delete();
        }
    }
}