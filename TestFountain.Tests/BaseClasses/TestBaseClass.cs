using Microsoft.Extensions.DependencyInjection;
using System;
using TestFountain.Tests.Fixtures;
using Xunit;

namespace TestFountain.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <seealso cref="IDisposable"/>
    [Collection("Canister collection")]
    public abstract class TestBaseClass
    {
        protected TestBaseClass()
        {
            if (Services is not null)
                return;
            Services = new ServiceCollection().AddCanisterModules(configure => configure.RegisterTestFountain().AddAssembly(typeof(CanisterFixture).Assembly)).BuildServiceProvider();
        }

        protected IServiceProvider Services { get; set; }
    }
}