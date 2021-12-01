using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataValueMemberSerializerTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = Member.Create<Dint>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = Member.Create<Dint>("Test");
            var serializer = new DataValueMemberSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
    }
}