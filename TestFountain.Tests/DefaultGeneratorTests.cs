using FluentAssertions;
using Mirage.Generators;
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
        public void Test([Range(1, 100)]int Value1)
        {
            Value1.Should().BeInRange(1, 100);
        }

        [Theory]
        [FountainData(1000)]
        public void Test2(TestClass Value1)
        {
            if (Value1 == null)
                return;
            Value1.Should().BeEquivalentTo(Value1.Copy());
            Value1.Value1.Should().BeGreaterThan(0);
            Value1.Value2.Should().NotBeNull();
            Value1.Value3.Should().BeGreaterThan(0);
        }

        [Theory]
        [FountainData(10, 400)]
        public void Test3(ITestInterface Value1)
        {
            if (Value1 == null)
                return;
            Value1.Method().Returns(1);
            Value1.Method().Should().Equals(1);
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