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

            Assert.True(TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]));
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new InterfaceGenerator();
            Assert.NotNull(TestObject);
        }

        [Fact]
        public void Next()
        {
            var TestObject = new InterfaceGenerator();
            var Result = (ITestInterface)TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            Assert.NotNull(Result);
            _ = Result.Method().Returns(1);
            Assert.Equal(1, Result.Method());
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