using System;
using System.IO;
using System.Xml.Linq;
using L5Sharp.Serialization;
using NUnit.Framework;

namespace L5Sharp.Factories.Tests
{
    [TestFixture]
    public class DataTypeFactoryTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private XDocument _document;
        private UserDefinedMaterializer _materializer;

        [SetUp]
        public void Setup()
        {
            _document = XDocument.Load(_fileName);
            
            _materializer = new UserDefinedMaterializer(null);
        }
        
    }
}