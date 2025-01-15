using System.Linq;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests
{
    public class FountainDataTests : TestBaseClass
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new FountainDataAttribute(1);
            Assert.NotNull(TestObject);
            Assert.Equal(1, TestObject.Count);
            Assert.Equal(int.MaxValue, TestObject.MaxDuration);
        }

        [Fact]
        public void GetData()
        {
            var TestObject = new FountainDataAttribute(1);
            System.Collections.Generic.IEnumerable<object[]> Data = TestObject.GetData(typeof(TestClass).GetMethod("TestMethod"));
            Assert.NotNull(Data);
            Assert.NotEmpty(Data);
            _ = Assert.Single(Data);
            Assert.NotNull(Data.First());
            Assert.NotEmpty(Data.First());
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}