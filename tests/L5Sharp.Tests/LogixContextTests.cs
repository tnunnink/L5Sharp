using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixContextTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }
        
        [Test]
        public void New_ValidFile_ShouldNotBeNull()
        {
            var sut = new LogixContext(_fileName);

            sut.Should().NotBeNull();
        }

        [Test]
        public void DataTypes_GetAll_ShouldNotBeEmpty()
        {
            var context = new LogixContext(_fileName);

            var types = context.DataTypes.GetAll();

            foreach (var type in types)
            {
                type.Should().NotBeNull();
            }
        }
    }
}