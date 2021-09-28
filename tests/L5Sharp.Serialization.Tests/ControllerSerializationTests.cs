using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class ControllerSerializationTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var controller = new Controller("ControllerName");
            
            var element = controller.Serialize();

            element.Should().NotBeNull();
        }

        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WhenCalled_ShouldHaveApprovedOutput()
        {
            var controller = new Controller("ControllerName");
            
            var element = controller.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WithDataTypes_ShouldHaveApprovedOutput()
        {
            var controller = new Controller("ControllerName");
            
            controller.Create().DataType("DT1", b =>
                b.HasDescription("Test Type 1")
                    .WithMember("TestMember", DataType.Bool));
            
            controller.Create().DataType("DT2", b =>
                b.HasDescription("Test Type 2")
                    .WithMember("TestMember", DataType.Dint));
            
            var element = controller.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
    }
}