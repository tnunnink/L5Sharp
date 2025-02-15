using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Types.Predefined
{
    [TestFixture]
    public class CounterTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new COUNTER();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new COUNTER();

            type.Name.Should().Be(nameof(COUNTER));
            type.Members.Should().HaveCount(7);
            type.PRE.Should().Be(0);
            type.ACC.Should().Be(0);
            type.CU.Should().Be(0);
            type.CD.Should().Be(0);
            type.DN.Should().Be(0);
            type.OV.Should().Be(0);
            type.UN.Should().Be(0);
        }

        [Test]
        public void New_Element_ShouldHaveExpectedValues()
        {
            const string xml = @"<StructureMember Name=""Member"" DataType=""COUNTER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""1234""/>
                <DataValueMember Name=""CU"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""CD"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""OV"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""UN"" DataType=""BOOL"" Value=""1""/>
                </StructureMember>";
            var element = XElement.Parse(xml);

            var type = new COUNTER(element);

            type.Should().NotBeNull();
            type.PRE.Should().Be(5000);
            type.ACC.Should().Be(1234);
            type.CU.Should().Be(1);
            type.CD.Should().Be(1);
            type.DN.Should().Be(1);
            type.OV.Should().Be(1);
            type.UN.Should().Be(1);
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new COUNTER();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}