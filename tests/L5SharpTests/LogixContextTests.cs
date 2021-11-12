using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using L5Sharp;
using NUnit.Framework;

namespace L5SharpTests
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

            var types = context.DataTypes.GetAll().ToList();

            types.Should().NotBeEmpty();
        }
        
        [Test]
        public void Tags_GetKnown_ShouldNotBeNull()
        {
            var context = new LogixContext(_fileName);

            var tag = context.Tags.Get("TestArrayTypeTag");

            tag.Should().NotBeNull();
        }
    }
}