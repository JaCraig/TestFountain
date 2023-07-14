using System.ComponentModel.DataAnnotations;
using Xunit;

namespace TestFountain.Example
{
    public class ExampleClass
    {
        /// <summary>
        /// Runs 10 tests with random values for a and b.
        /// </summary>
        /// <param name="a">a, values will be in the range of 0 to 1000.</param>
        /// <param name="b">b, values will be in the range of 0 to 1000.</param>
        [Theory]
        [FountainData(10)]
        public void Method1([Range(0, 1000)] int a, [Range(0, 1000)] int b)
        {
            Assert.True(0 <= a && a <= 1000);
            Assert.True(0 <= b && b <= 1000);
        }
    }
}