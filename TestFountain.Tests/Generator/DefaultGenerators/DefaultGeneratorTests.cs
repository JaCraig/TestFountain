using FluentAssertions;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator.DefaultGenerators
{
    public class DefaultGeneratorTests : TestBaseClass
    {
        [Fact]
        public void CanGenerate()
        {
            var TestObject = new DefaultGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());

            TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
            TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new DefaultGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Result.Should().BeOfType<string>();
            Result.Should().NotBeNull();
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}