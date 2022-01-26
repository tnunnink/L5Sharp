using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ControllerSerializerTests
    {
        private ControllerSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ControllerSerializer();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var controller = new Controller("Test", ProcessorType.L74, new Revision());

            var serialized = _serializer.Serialize(controller);

            serialized.Should().NotBeNull();
        }
        
        /*[Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WhenCalled_ShouldNotBeApproved()
        {
            var controller = new Controller("Test", ProcessorType.L74, new Revision());

            var serialized = _serializer.Serialize(controller);

            Approvals.VerifyXml(serialized.ToString());
        }*/
    }
}