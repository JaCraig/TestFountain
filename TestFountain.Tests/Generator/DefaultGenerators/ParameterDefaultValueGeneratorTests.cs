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

            Assert.True(TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]));
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new ParameterDefaultValueGenerator();
            Assert.NotNull(TestObject);
        }

        [Fact]
        public void Next()
        {
            var TestObject = new ParameterDefaultValueGenerator();
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Assert.Equal("Test Value", Result);
        }

        private class TestClass
        {
            public void TestMethod(string a = "Test Value")
            {
            }
        }
    }
}