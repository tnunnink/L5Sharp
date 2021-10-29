using System;
using System.IO;
using System.Linq;
using FluentAssertions;
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

            var types = context.DataTypes.GetAll().ToList();

            types.Should().NotBeEmpty();
        }

        [Test]
        public void DataTypes_GetAll_SimpleTypesReferenceSame()
        {
            var context = new LogixContext(_fileName);

            var types = context.DataTypes.GetAll().ToList();

            var complex = types.SingleOrDefault(t => t.Name == "ComplexType");
            var simple = types.SingleOrDefault(t => t.Name == "SimpleTypes");

            var simpleMember = complex?.Members.Single(m => m.Name == "SimpleMember").DataType;

            simple.Should().BeEquivalentTo(simpleMember);
            simple.Should().BeSameAs(simpleMember);
        }
    }
}