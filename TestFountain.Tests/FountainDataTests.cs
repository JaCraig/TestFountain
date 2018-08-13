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
            TestObject.Should().NotBeNull();
            TestObject.Count.Should().Be(1);
            TestObject.MaxDuration.Should().Be(int.MaxValue);
        }

        [Fact]
        public void GetData()
        {
            var TestObject = new FountainDataAttribute(1);
            var Data = TestObject.GetData(typeof(TestClass).GetMethod("TestMethod"));
            Data.Should().NotBeNullOrEmpty();
            Data.Should().ContainSingle();
            Data.First().Should().NotBeNullOrEmpty();
        }

        private class TestClass
        {
            public void TestMethod(string a)
            {
            }
        }
    }
}