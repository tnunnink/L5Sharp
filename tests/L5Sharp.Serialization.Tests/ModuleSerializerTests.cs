using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ModuleSerializerTests
    {
        private static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private ModuleSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            var context = new LogixContext(L5X);
            _serializer = new ModuleSerializer(context);
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        /*[Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var module = new Module("Test", "Catalog", 0, 1, 1);

            var xml = _serializer.Serialize(module);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValueTypeArray_ShouldBeApproved()
        {
            var module = new Module("Test", "Catalog", 0, 1, 1);

            var xml = _serializer.Serialize(module);

            Approvals.VerifyXml(xml.ToString());
        }*/
    }
}