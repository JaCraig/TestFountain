using Mirage.Generators;
using Mirage.Generators.Names;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests
{
    public interface ITestInterface
    {
        int Method();
    }

    public class DefaultGeneratorTests : TestBaseClass
    {
        [Theory]
        [FountainData(100, 1000)]
        public void Test([Range(1, 100)] int value1) =>
            Assert.InRange(value1, 1, 100);

        [Theory]
        [FountainData(1000)]
        public void Test2(TestClass value1)
        {
            if (value1 == null)
                return;
            Assert.InRange(value1.Value1, 1, int.MaxValue);
            Assert.NotNull(value1.Value2);
            Assert.InRange(value1.Value3, 1f, float.MaxValue);
        }

        [Theory]
        [FountainData(10, 400)]
        public void Test3(ITestInterface value1)
        {
            if (value1 == null)
                return;
            _ = value1.Method().Returns(1);
            Assert.Equal(1, value1.Method());
        }
    }

    public class TestClass
    {
        [IntGenerator(1, int.MaxValue)]
        public int Value1 { get; set; }

        [Company]
        public string Value2 { get; set; }

        [FloatGenerator(1f, float.MaxValue)]
        public float Value3 { get; set; }

        public TestClass Copy()
        {
            return new TestClass
            {
                Value1 = Value1,
                Value2 = Value2,
                Value3 = Value3
            };
        }
    }
}