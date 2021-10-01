using System;
using System.IO;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Loaders;
using L5Sharp.Primitives;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    [TestFixture]
    public class DataTypeLoaderTests
    {
        private Controller _controller;
        private static readonly string FileName = Path.Combine(Environment.CurrentDirectory, @"TestData\Test.xml");

        [SetUp]
        public void Setup()
        {
            _controller = new Controller("Test");
        }
        
        [Test]
        public void TestDataFileExists()
        {
            FileAssert.Exists(FileName);
        }
        
        [Test]
        public void Load_WhenCalled_ShouldUpdateController()
        {
            var loader = new DataTypeLoader(_controller);
            var doc = XDocument.Load(FileName);
            var elements = doc.Descendants(L5XNames.DataType);
            
            loader.Load(elements);

            _controller.DataTypes.Should().NotBeEmpty();
        }
    }
}