using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests;

public class LogixDataTests
{
    /*[Test]
    public void Register_ValidType_ShouldBeRegistered()
    {
        LogixData.ScanMode = ScanMode.None;

        LogixData.Register<MySimpleType>();

        LogixData.IsRegistered(nameof(MySimpleType)).Should().BeTrue();
    }
    
    [Test]
    public void Scan_ValidAssembly_ShouldBeRegistered()
    {
        LogixData.ScanMode = ScanMode.None;

        LogixData.Scan(typeof(MySimpleType).Assembly);

        LogixData.IsRegistered(nameof(MySimpleType)).Should().BeTrue();
        LogixData.IsRegistered(nameof(MyNestedType)).Should().BeTrue();
    }*/

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

        var type = LogixData.Deserialize(element);

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
        type.As<TIMER>().PRE.Should().Be(5000);
        type.As<TIMER>().ACC.Should().Be(1234);
        type.As<TIMER>().EN.Should().Be(1);
        type.As<TIMER>().TT.Should().Be(1);
        type.As<TIMER>().DN.Should().Be(1);
    }

    /*
    [Test]
    public void Deserialize_NoRegisteredType_ShouldBeComplexType()
    {
        const string xml = @"<StructureMember Name=""TimeMember"" DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""1234""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1""/>
                </StructureMember>";
        var element = XElement.Parse(xml);

        var type = LogixData.Deserialize(element);

        type.Should().BeOfType<ComplexType>();
    }*/
}