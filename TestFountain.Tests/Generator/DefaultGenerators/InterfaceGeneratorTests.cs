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

            _ = TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]).Should().BeTrue();
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new InterfaceGenerator();
            _ = TestObject.Should().NotBeNull();
        }

        [Fact]
        public void Next()
        {
            var TestObject = new InterfaceGenerator();
            var Result = (ITestInterface)TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            _ = Result.Value.Should().NotBeNull();
            _ = Result.Method().Returns(1);
            _ = Result.Method().Should().Be(1);
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