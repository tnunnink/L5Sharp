using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
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
        public void Serialize_WhenCalled_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WithMembers_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            type.Members.Add(new DataTypeMember("Member_Bool", Predefined.Bool));
            type.Members.Add(new DataTypeMember("Member_Sint", Predefined.Sint));
            type.Members.Add(new DataTypeMember("Member_Int", Predefined.Int));
            type.Members.Add(new DataTypeMember("Member_Dint", Predefined.Dint));
            type.Members.Add(new DataTypeMember("Member_Lint", Predefined.Lint));
            type.Members.Add(new DataTypeMember("Member_Real", Predefined.Real));
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
    }
}