using FluentAssertions;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator.DefaultGenerators
{
    public class DefaultValueGeneratorTests : TestBaseClass
    {
        [Fact]
        public void CanGenerate()
        {
            var TestObject = new DefaultValueGenerator();

            TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultValueGenerator();
            TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new DefaultValueGenerator();
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Result.Should().BeNull();
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}