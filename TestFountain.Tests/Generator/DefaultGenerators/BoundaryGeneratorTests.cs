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

            Assert.True(TestObject.CanGenerate(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]));
        }

        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultGenerator(Services.GetService<Mirage.Random>());
            Assert.NotNull(TestObject);
        }

        [Fact]
        public void Next()
        {
            var TestObject = new DefaultGenerator(Services.GetService<Mirage.Random>());
            var Result = TestObject.Next(typeof(TestClass).GetMethod("TestMethod").GetParameters()[0]);
            _ = Assert.IsType<char>(Result);
        }

        private class TestClass
        {
            public void TestMethod(char a)
            {
            }
        }
    }
}