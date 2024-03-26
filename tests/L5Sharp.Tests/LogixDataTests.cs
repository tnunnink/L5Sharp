using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests;

public class LogixDataTests
{
    [Test]
    public void Deserialize_RegisteredType_ShouldWork()
    {
        const string xml = @"<StructureMember Name=""TimeMember"" DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""1234""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1""/>
                </StructureMember>";
        var element = XElement.Parse(xml);

        var type = element.Deserialize<TIMER>();

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
        type.PRE.Should().Be(5000);
        type.ACC.Should().Be(1234);
        type.EN.Should().Be(1);
        type.TT.Should().Be(1);
        type.DN.Should().Be(1);
    }
}