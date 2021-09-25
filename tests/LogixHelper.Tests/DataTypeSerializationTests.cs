using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using LogixHelper.Primitives;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class DataTypeSerializationTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var type = new DataType("TypeName");
            
            var element = type.Serialize();

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialise_WhenCalled_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialise_WithMembers_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            type.AddMember("Member_Bool", DataType.Bool);
            type.AddMember("Member_Sint", DataType.Sint);
            type.AddMember("Member_Int", DataType.Int);
            type.AddMember("Member_Dint", DataType.Dint);
            type.AddMember("Member_Lint", DataType.Lint);
            type.AddMember("Member_Real", DataType.Real);
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
    }
}