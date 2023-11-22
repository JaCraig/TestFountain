using FluentAssertions;
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
            _ = TestObject.Should().NotBeNull();
            _ = TestObject.Count.Should().Be(1);
            _ = TestObject.MaxDuration.Should().Be(int.MaxValue);
        }

        [Fact]
        public void GetData()
        {
            var TestObject = new FountainDataAttribute(1);
            System.Collections.Generic.IEnumerable<object[]> Data = TestObject.GetData(typeof(TestClass).GetMethod("TestMethod"));
            _ = Data.Should().NotBeNullOrEmpty();
            _ = Data.Should().ContainSingle();
            _ = Data.First().Should().NotBeNullOrEmpty();
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}