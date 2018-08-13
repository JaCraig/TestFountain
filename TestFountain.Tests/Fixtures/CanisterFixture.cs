using FileCurator;
using System;
using System.Reflection;
using TestFountain.Registration;
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
            if (Canister.Builder.Bootstrapper == null)
            {
                Canister.Builder.CreateContainer(null, typeof(CanisterFixture).GetTypeInfo().Assembly)
                   .RegisterTestFountain()
                   .Build();
            }
        }

        public void Dispose()
        {
            new DirectoryInfo("./TestFountain/SavedTests/TestFountain.Tests.Generator/").Delete();
            new DirectoryInfo("./TestFountain/SavedTests/TestFountain.Tests.DataSources/").Delete();
        }
    }
}