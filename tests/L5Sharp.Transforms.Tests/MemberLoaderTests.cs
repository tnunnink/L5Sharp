using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Loaders;
using L5Sharp.Primitives;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    [TestFixture]
    public class MemberLoaderTests
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
            var doc = XDocument.Load(FileName);
            var dataTypes = doc.Descendants(L5XNames.DataType);
            var typeLoader = new DataTypeLoader(_controller);
            typeLoader.Load(dataTypes);
            
            var elements = doc.Descendants(L5XNames.Member);
            var loader = new MemberLoader(_controller);
            loader.Load(elements);

            _controller.DataTypes.All(x => x.Members != null).Should().BeTrue();
        }
    }
}