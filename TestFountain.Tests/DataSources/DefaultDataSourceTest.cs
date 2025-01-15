using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using TestFountain.DataSources;
using TestFountain.Tests.BaseClasses;
using Xunit;

namespace TestFountain.Tests.DataSources
{
    public class DefaultDataSourceTest : TestBaseClass
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new DefaultDataSource(Services.GetService<SerialBox.SerialBox>());
            Assert.NotNull(TestObject);
            Assert.NotNull(TestObject.SerialBox);
        }

        [Fact]
        public void Read()
        {
            var TestObject = new DefaultDataSource(Services.GetService<SerialBox.SerialBox>());
            TestObject.Save(typeof(DefaultDataSourceTestClassRead).GetMethod("TestMethod"), new object[] { "A" });
            TestObject.Save(typeof(DefaultDataSourceTestClassRead).GetMethod("TestMethod"), new object[] { "B" });
            TestObject.Save(typeof(DefaultDataSourceTestClassRead).GetMethod("TestMethod"), new object[] { "C" });

            _ = TestObject.Read(typeof(DefaultDataSourceTestClassRead).GetMethod("TestMethod"));
            //var Expected = new List<object[]>
            //{
            //    new object[] { "A" },
            //    new object[] { "B" },
            //    new object[] { "C" }
            //};
            //Results.Should().BeEquivalentTo(Expected);
        }

        [Fact]
        public void Save()
        {
            var TestObject = new DefaultDataSource(Services.GetService<SerialBox.SerialBox>());
            TestObject.Save(typeof(DefaultDataSourceTestClassSave).GetMethod("TestMethod"), new object[] { "A" });

            _ = new DirectoryInfo("./TestFountain/SavedTests/TestFountain.Tests.DataSources/DefaultDataSourceTest.DefaultDataSourceTestClassSave/TestMethod/");
            //TestDataDirectory.EnumerateDirectories().Should().ContainSingle();
            //TestDataDirectory.EnumerateFiles(options: System.IO.SearchOption.AllDirectories).Should().ContainSingle();
            //TestDataDirectory.EnumerateFiles(options: System.IO.SearchOption.AllDirectories).FirstOrDefault()?.Read().Should().Be("\"A\"");
        }

        private class DefaultDataSourceTestClassRead
        {
            public void TestMethod(string value)
            {
            }
        }

        private class DefaultDataSourceTestClassSave
        {
            public void TestMethod(string value)
            {
            }
        }
    }
}