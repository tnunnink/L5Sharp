using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5XParser.Tests
{
    [TestFixture]
    public class L5XFileTests
    {

        [Test]
        public void TestFileExists()
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\ETBB Master Template Gen5.xml");

            File.Exists(fileName).Should().BeTrue();
        }
        
        [Test]
        public void New_ValidL5XFile_ShouldNotBeNull()
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\ETBB Master Template Gen5.xml");

            var sut = new L5XFile(fileName);

            sut.Should().NotBeNull();
        }

        [Test]
        public void GetModules_WhenCalled_ShouldNotBeNullOrEmpty()
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\ETBB Master Template Gen5.xml");
            var sut = new L5XFile(fileName);

            var modules = sut.GetModules().ToList();

            modules.Should().NotBeNull();
            modules.Should().NotBeEmpty();
        }

        [Test]
        public void GetRungs_WhenCalled_ShouldNotBeNull()
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\ETBB Master Template Gen5.xml");
            var sut = new L5XFile(fileName);

            var rungs = sut.GetRungs().ToList();

            rungs.Should().NotBeNull();
            rungs.Should().NotBeEmpty();
        }
        
        [Test]
        public void GetRungsWithTag_KnownValidTag_ShouldNotBeNullOrEmpty()
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\ETBB Master Template Gen5.xml");
            var sut = new L5XFile(fileName);

            var rungs = sut.GetRungsWithTag("FIO_INJ:1:I").ToList();

            rungs.Should().NotBeNull();
            rungs.Should().NotBeEmpty();
        }
    }
}