﻿using TestFountain.Generator.DefaultGenerators;
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

            Assert.True(TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]));
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultValueGenerator();
            Assert.NotNull(TestObject);
        }

        [Fact]
        public void Next()
        {
            var TestObject = new DefaultValueGenerator();
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Assert.Null(Result);
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}