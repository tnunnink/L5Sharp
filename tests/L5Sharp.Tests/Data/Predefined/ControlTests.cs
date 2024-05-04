using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class ControlTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new CONTROL();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new CONTROL();

            type.Name.Should().Be(nameof(CONTROL));
            type.Members.Should().HaveCount(10);
            type.LEN.Should().Be(0);
            type.POS.Should().Be(0);
            type.EN.Should().Be(0);
            type.EU.Should().Be(0);
            type.DN.Should().Be(0);
            type.EM.Should().Be(0);
            type.ER.Should().Be(0);
            type.UL.Should().Be(0);
            type.IN.Should().Be(0);
            type.FD.Should().Be(0);
        }
        
        [Test]
        public void New_Element_ShouldHaveExpectedValues()
        {
            const string xml = @"<StructureMember Name=""Member"" DataType=""CONTROL"">
                <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                <DataValueMember Name=""POS"" DataType=""DINT"" Radix=""Decimal"" Value=""1234""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""EU"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""EM"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""ER"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""UL"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""IN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""FD"" DataType=""BOOL"" Value=""1""/>
                </StructureMember>";
            var element = XElement.Parse(xml);
            
            var type = new CONTROL(element);

            type.Should().NotBeNull();
            type.LEN.Should().Be(5000);
            type.POS.Should().Be(1234);
            type.EN.Should().Be(1);
            type.EU.Should().Be(1);
            type.DN.Should().Be(1);
            type.EM.Should().Be(1);
            type.ER.Should().Be(1);
            type.UL.Should().Be(1);
            type.IN.Should().Be(1);
            type.FD.Should().Be(1);
        }
        
        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new CONTROL();
            
            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}