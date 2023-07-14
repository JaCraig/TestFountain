using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator.DefaultGenerators
{
    public class BoundaryGeneratorTests : TestBaseClass
    {
        [Fact]
        public void CanGenerate()
        {
            var TestObject = new BoundaryGenerator(Services.GetService<Mirage.Random>());

            TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultGenerator(Services.GetService<Mirage.Random>());
            TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new DefaultGenerator(Services.GetService<Mirage.Random>());
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Result.Should().BeOfType<char>();
        }

        private class TestClass
        {
            public void TestMethod(char a)
            {
            }
        }
    }
}