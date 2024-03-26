using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new MESSAGE();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new MESSAGE();

            type.Name.Should().Be(nameof(MESSAGE));
            type.Members.Should().HaveCount(13);
            type.MessageType.Should().BeEquivalentTo("");
            type.RequestedLength.Should().BeEquivalentTo(0);
            type.ConnectedFlag.Should().BeEquivalentTo(0);
            type.ConnectionPath.Should().BeEquivalentTo("");
            type.CommTypeCode.Should().BeEquivalentTo(0);
            type.ServiceCode.Should().BeEquivalentTo(0);
            type.ObjectType.Should().BeEquivalentTo(0);
            type.TargetObject.Should().BeEquivalentTo(0);
            type.AttributeNumber.Should().BeEquivalentTo(0);
            type.LocalIndex.Should().BeEquivalentTo(0);
            type.DestinationTag.Should().BeEquivalentTo("");
            type.CacheConnections.Should().BeEquivalentTo(0);
            type.LargePacketUsage.Should().BeEquivalentTo(0);
        }

        [Test]
        public void New_Element_ShouldHaveExpectedValues()
        {
            const string xml =
                @"<MessageParameters MessageType=""CIP Generic"" RequestedLength=""0"" ConnectedFlag=""1"" ConnectionPath=""ETAP"" 
                                    CommTypeCode=""0"" ServiceCode=""16#0001"" ObjectType=""16#0047"" TargetObject=""1"" AttributeNumber=""16#0000""
                                     LocalIndex=""0"" DestinationTag=""SomeTagName"" CacheConnections=""FALSE"" LargePacketUsage=""false""/>";
            var element = XElement.Parse(xml);

            var type = new MESSAGE(element);

            type.Should().NotBeNull();
            type.MessageType.Should().BeEquivalentTo("CIP Generic");
            type.RequestedLength.Should().Be(0);
            type.ConnectedFlag.Should().Be(1);
            type.ConnectionPath.Should().BeEquivalentTo("ETAP");
            type.CommTypeCode.Should().Be(0);
            type.ServiceCode.Should().Be(1);
            type.ObjectType.Should().Be(71);
            type.TargetObject.Should().Be(1);
            type.AttributeNumber.Should().Be(0);
            type.LocalIndex.Should().Be(0);
            type.DestinationTag.Should().BeEquivalentTo("SomeTagName");
            type.CacheConnections.Should().BeEquivalentTo(0);
            type.LargePacketUsage.Should().BeEquivalentTo(0);
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new MESSAGE();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}