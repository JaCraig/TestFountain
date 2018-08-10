using Mirage.Generators;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.Generator
{
    public class DefaultGeneratorTests : TestBaseClass
    {
        [Theory]
        [FountainData(1000)]
        public void Test(int Value1)
        {
            Assert.InRange(Value1, int.MinValue, int.MaxValue);
        }

        [Theory]
        [FountainData(1000)]
        public void Test2(TestClass Value1)
        {
            if (Value1 == null)
                return;
            Assert.True(Value1.Value1 > 0);
            Assert.True(Value1.Value2 != null);
            Assert.True(Value1.Value3 > 0);
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
    }
}