using System;
using System.IO;
using FluentAssertions;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class L5XTests
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
            var sut = new Logix(_fileName);

            sut.Should().NotBeNull();
        }
    }
}