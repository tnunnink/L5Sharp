using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new MessageData();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new MessageData();

            type.Name.Should().Be("MESSAGE");
            type.Members.Should().BeEmpty();
            type.MessageType.Should().Be(MessageType.Unconfigured);
            type.RequestedLength.Should().Be(0);
            type.ConnectedFlag.Should().Be(0);
            type.ConnectionPath.Should().BeNull();
            type.CommTypeCode.Should().Be(0);
            type.ServiceCode.Should().Be(0);
            type.ObjectType.Should().Be(0);
            type.TargetObject.Should().Be(0);
            type.AttributeNumber.Should().Be(0);
            type.LocalIndex.Should().Be(0);
            type.DestinationTag.Should().Be(TagName.Empty);
            type.CacheConnections.Should().BeNull();
            type.LargePacketUsage.Should().BeNull();
        }

        [Test]
        public void New_Element_ShouldHaveExpectedValues()
        {
            const string xml =
                """
                <MessageParameters MessageType="CIP Generic" RequestedLength="0" ConnectedFlag="1" 
                    ConnectionPath="ETAP" CommTypeCode="0" ServiceCode="16#0001" ObjectType="16#0047" TargetObject="1" 
                    AttributeNumber="16#0000" LocalIndex="0" DestinationTag="SomeTagName" 
                    CacheConnections="FALSE" LargePacketUsage="false"/>
                """;
            
            var element = XElement.Parse(xml);

            var type = new MessageData(element);

            type.Should().NotBeNull();
            type.MessageType.Should().Be(MessageType.CIPGeneric);
            type.RequestedLength.Should().Be(0);
            type.ConnectedFlag.Should().Be(1);
            type.ConnectionPath.Should().Be("ETAP");
            type.CommTypeCode.Should().Be(0);
            type.ServiceCode.Should().Be(1);
            type.ObjectType.Should().Be(71);
            type.TargetObject.Should().Be(1);
            type.AttributeNumber.Should().Be(0);
            type.LocalIndex.Should().Be(0);
            type.DestinationTag.Should().Be("SomeTagName");
            type.CacheConnections.Should().Be(false);
            type.LargePacketUsage.Should().Be(false);
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new MessageData();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}