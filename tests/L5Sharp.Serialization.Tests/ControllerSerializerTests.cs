using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ControllerSerializerTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var controller = Controller.Create("Test", ProcessorType.L74, new Revision());

            var serialized = controller.Serialize();

            serialized.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WhenCalled_ShouldNotBeApproved()
        {
            var controller = Controller.Create("Test", ProcessorType.L74, new Revision());

            var serialized = controller.Serialize();

            Approvals.VerifyXml(serialized.ToString());
        }
    }
}