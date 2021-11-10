using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataTypeSerializationTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var type = new DataType("Test", new[] { Member.New("Test", new Dint()) });
            
            var element = type.Serialize();

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WhenCalled_ShouldHaveApprovedOutput()
        {
            var type = new DataType("Test", new[] { Member.New("Test", new Dint()) });
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WithMembers_ShouldHaveApprovedOutput()
        {
            var type = new DataType("Test", new[] { Member.New("Test", new Dint()) });
            type.Members.Add(Member.New("Member_Bool", new Bool()));
            type.Members.Add(Member.New("Member_Sint", new Sint()));
            type.Members.Add(Member.New("Member_Int", new Int()));
            type.Members.Add(Member.New("Member_Dint", new Dint()));
            type.Members.Add(Member.New("Member_Lint", new Lint()));
            type.Members.Add(Member.New("Member_Real", new Real()));
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
    }
}