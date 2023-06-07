using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class LogixTypeTests
{
    [Test]
    public void Null_WhenCalled_ShouldBeNullType()
    {
        var type = LogixType.Null;

        type.Should().BeOfType<NullType>();
    }

    [Test]
    [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Decimal\" Value=\"1\"/>")]
    [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Binary\" Value=\"2#1\"/>")]
    [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Octal\" Value=\"8#1\"/>")]
    [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Hex\" Value=\"16#1\"/>")]
    public void Deserialize_BoolAsDecimal_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<BOOL>();

        type.Should().BeOfType<BOOL>();
        type.Name.Should().Be("BOOL");
        type.Should().Be(true);
    }

    [Test]
    [TestCase("<DataValue DataType=\"SINT\" Radix=\"Decimal\" Value=\"12\"/>")]
    [TestCase("<DataValue DataType=\"SINT\" Radix=\"Binary\" Value=\"2#0000_1100\"/>")]
    [TestCase("<DataValue DataType=\"SINT\" Radix=\"Octal\" Value=\"8#014\"/>")]
    [TestCase("<DataValue DataType=\"SINT\" Radix=\"Hex\" Value=\"16#0c\"/>")]
    [TestCase("<DataValue DataType=\"SINT\" Radix=\"ASCII\" Value=\"'$p'\"/>")]
    public void Deserialize_Sint_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<SINT>();

        type.Should().BeOfType<SINT>();
        type.Name.Should().Be("SINT");
        type.Should().Be(12);
    }

    [Test]
    [TestCase("<DataValue DataType=\"INT\" Radix=\"Decimal\" Value=\"4321\"/>")]
    [TestCase("<DataValue DataType=\"INT\" Radix=\"Binary\" Value=\"2#0001_0000_1110_0001\"/>")]
    [TestCase("<DataValue DataType=\"INT\" Radix=\"Octal\" Value=\"8#010_341\"/>")]
    [TestCase("<DataValue DataType=\"INT\" Radix=\"Hex\" Value=\"16#10e1\"/>")]
    [TestCase("<DataValue DataType=\"INT\" Radix=\"ASCII\" Value=\"'$10$E1'\"/>")]
    public void Deserialize_Int_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<INT>();

        type.Should().BeOfType<INT>();
        type.Name.Should().Be("INT");
        type.Should().Be(4321);
    }

    [Test]
    [TestCase("<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"123456\"/>")]
    [TestCase("<DataValue DataType=\"DINT\" Radix=\"Binary\" Value=\"2#0000_0000_0000_0001_1110_0010_0100_0000\"/>")]
    [TestCase("<DataValue DataType=\"DINT\" Radix=\"Octal\" Value=\"8#00_000_361_100\"/>")]
    [TestCase("<DataValue DataType=\"DINT\" Radix=\"Hex\" Value=\"16#0001_e240\"/>")]
    [TestCase("<DataValue DataType=\"DINT\" Radix=\"ASCII\" Value=\"'$00$01$E2@'\"/>")]
    public void Deserialize_Dint_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<DINT>();

        type.Should().BeOfType<DINT>();
        type.Name.Should().Be("DINT");
        type.Should().Be(123456);
    }

    [Test]
    [TestCase("<DataValue DataType=\"LINT\" Radix=\"Decimal\" Value=\"112230123\"/>")]
    [TestCase(
        "<DataValue DataType=\"LINT\" Radix=\"Binary\" Value=\"2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0110_1011_0000_0111_1110_1110_1011\"/>")]
    [TestCase("<DataValue DataType=\"LINT\" Radix=\"Octal\" Value=\"8#0_000_000_000_000_654_077_353\"/>")]
    [TestCase("<DataValue DataType=\"LINT\" Radix=\"Hex\" Value=\"16#0000_0000_06b0_7eeb\"/>")]
    [TestCase("<DataValue DataType=\"LINT\" Radix=\"ASCII\" Value=\"'$00$00$00$00$06$B0~$EB'\"/>")]
    [TestCase("<DataValue DataType=\"LINT\" Radix=\"ASCII\" Value=\"DT#1970-01-01-00:01:52.230_123Z\"/>")]
    public void Deserialize_Lint_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<LINT>();

        type.Should().BeOfType<LINT>();
        type.Name.Should().Be("LINT");
        type.Should().Be(112230123);
    }

    [Test]
    [TestCase("<DataValue DataType=\"REAL\" Radix=\"Float\" Value=\"1.23\"/>")]
    [TestCase("<DataValue DataType=\"REAL\" Radix=\"Exponential\" Value=\"1.23000000e+000\"/>")]
    public void Deserialize_Real_ShouldBeExpected(string xml)
    {
        var element = XElement.Parse(xml);

        var type = LogixType.Deserialize(element).To<REAL>();

        type.Should().BeOfType<REAL>();
        type.Name.Should().Be("REAL");
        type.Should().Be(1.23f);
    }

    [Test]
    public void IsRegistered_ByName_ShouldBeTrue()
    {
        var result = LogixType.IsRegistered(nameof(TIMER));

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_ByType_ShouldBeTrue()
    {
        var result = LogixType.IsRegistered<TIMER>();

        result.Should().BeTrue();
    }

    [Test]
    public void Register_MyNestedType_ShouldBeRegistered()
    {
        LogixType.Register<MyNestedType>();

        var result = LogixType.IsRegistered(nameof(MyNestedType));

        result.Should().BeTrue();
    }

    [Test]
    public void Scan_TestAssembly_ShouldHaveCustomTypesRegistered()
    {
        LogixType.Scan(typeof(MySimpleType).Assembly);

        LogixType.IsRegistered<MySimpleType>().Should().BeTrue();
        LogixType.IsRegistered<MyNestedType>().Should().BeTrue();
    }

    [Test]
    public void ScanAll_WhenCalled_ShouldHaveCustomTypesRegistered()
    {
        LogixType.ScanAll();

        LogixType.IsRegistered<MySimpleType>().Should().BeTrue();
        LogixType.IsRegistered<MyNestedType>().Should().BeTrue();
    }
}