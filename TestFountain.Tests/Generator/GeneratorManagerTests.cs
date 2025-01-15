using System;
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
            var TestObject = new GeneratorManager(Array.Empty<IGenerator>(), new Mirage.Random());
            Assert.NotNull(TestObject);
            Assert.Empty(TestObject.Generators);
        }

        [Fact]
        public void Next()
        {
            var TestObject = new GeneratorManager(new IGenerator[] { new DefaultGenerator(new Mirage.Random()) }, new Mirage.Random());
            var Results = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters());
            Assert.NotNull(Results);
            _ = Assert.Single(Results);
            Assert.NotNull(Results[0]);
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}