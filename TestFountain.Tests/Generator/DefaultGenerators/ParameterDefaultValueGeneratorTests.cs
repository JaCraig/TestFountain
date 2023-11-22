using FluentAssertions;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator.DefaultGenerators
{
    public class ParameterDefaultValueGeneratorTests : TestBaseClass
    {
        [Fact]
        public void CanGenerate()
        {
            var TestObject = new ParameterDefaultValueGenerator();

            _ = TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new ParameterDefaultValueGenerator();
            _ = TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new ParameterDefaultValueGenerator();
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            _ = Result.Should().Be("Test Value");
        }

        private class TestClass
        {
            public void TestMethod(string a = "Test Value")
            {
            }
        }
    }
}