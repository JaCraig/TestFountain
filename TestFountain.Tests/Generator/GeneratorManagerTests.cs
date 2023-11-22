using FluentAssertions;
using TestFountain.Generator;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Generator.Interfaces;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator
{
    public class GeneratorManagerTests : TestBaseClass
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new GeneratorManager(new IGenerator[0], new Mirage.Random());
            _ = TestObject.Generators.Should().BeEmpty();
            _ = TestObject.Random.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new GeneratorManager(new IGenerator[] { new DefaultGenerator(new Mirage.Random()) }, new Mirage.Random());
            var Results = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters());
            _ = Results.Should().NotBeNull();
            _ = Results.Should().HaveCount(1);
            _ = Results[0].Should().NotBeNull();
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}