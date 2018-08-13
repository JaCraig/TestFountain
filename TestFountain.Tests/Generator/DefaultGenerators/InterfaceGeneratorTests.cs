using FluentAssertions;
using NSubstitute;
using TestFountain.Generator.DefaultGenerators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Interface generator tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class InterfaceGeneratorTests : TestBaseClass
    {
        [Fact]
        public void CanGenerate()
        {
            var TestObject = new InterfaceGenerator();

            TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new InterfaceGenerator();
            TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new InterfaceGenerator();
            ITestInterface Result = (ITestInterface)TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Result.Value.Should().NotBeNull();
            Result.Method().Returns(1);
            Result.Method().Should().Equals(1);
        }

        public interface ITestInterface
        {
            string Value { get; set; }

            int Method();
        }

        private class TestClass
        {
            public void TestMethod(ITestInterface test)
            {
            }
        }
    }
}