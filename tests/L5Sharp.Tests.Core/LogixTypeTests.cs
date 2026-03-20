using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Tests.Core.Data.Custom;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixTypeTests
{
    [Test]
    public void Null_WhenCalled_ShouldBeExpected()
    {
        var type = LogixType.Null;

        type.Should().NotBeNull();
        type.Name.Should().Be("NULL");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void Register_ValidType_ShouldBeRegistered()
    {
        LogixType.Register<MyUnregisteredType>("MyTypeName");

        var result = LogixType.IsRegistered("MyTypeName");

        result.Should().BeTrue();
    }

    [Test]
    public void Register_EmptyKey_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Register<MySimpleData>(string.Empty))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Create_BOOL_ShouldBeExpected()
    {
        var type = LogixType.Create("BOOL");

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().Be(false);
    }

    [Test]
    public void Create_SINT_ShouldBeExpected()
    {
        var type = LogixType.Create("SINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<SINT>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_INT_ShouldBeExpected()
    {
        var type = LogixType.Create("INT");

        type.Should().NotBeNull();
        type.Should().BeOfType<INT>();
        type.Should().BeEquivalentTo(new INT());
        type.Should().Be(0);
    }

    [Test]
    public void Create_DINT_ShouldBeExpected()
    {
        var type = LogixType.Create("DINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<DINT>();
        type.Should().BeEquivalentTo(new DINT());
        type.Should().Be(0);
    }

    [Test]
    public void Create_LINT_ShouldBeExpected()
    {
        var type = LogixType.Create("LINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<LINT>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_REAL_ShouldBeExpected()
    {
        var type = LogixType.Create("REAL");

        type.Should().NotBeNull();
        type.Should().BeOfType<REAL>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_TIMER_ShouldBeExpected()
    {
        var type = LogixType.Create("TIMER");

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
        type.Members.Should().HaveCount(5);
    }

    [Test]
    public void Create_NonRegistered_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Create("Fake"))
            .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Create_RegisteredGenericOverload_ShouldBeNotNullAndExpectedType()
    {
        var type = LogixType.Create<BOOL>();

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().Be(false);
    }

    [Test]
    public void TryCreate_RegisteredAtomic_ShouldBeTrue()
    {
        var result = LogixType.TryCreate("DINT", out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Should().BeOfType<DINT>();
    }

    [Test]
    public void TryCreate_RegisteredPredefined_ShouldBeTrue()
    {
        var result = LogixType.TryCreate("COUNTER", out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Should().BeOfType<COUNTER>();
    }

    [Test]
    public void Deserialize_DataValueElement_ShouldBeExpectedType()
    {
        var xml = XElement.Parse(
            """
            <DataValue DataType="BOOL" Radix="Decimal" Value="0"/>
            """
        );

        var data = LogixType.Deserialize(xml);

        data.Should().NotBeNull();
        data.Should().BeOfType<BOOL>();
        data.Should().Be(false);
    }

    [Test]
    public void Deserialize_StructureElement_ShouldBeExpectedType()
    {
        var xml = XElement.Parse(
            """
            <Structure DataType="TIMER">
                <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0"/>
                <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                <DataValueMember Name="EN" DataType="BOOL" Value="0"/>
                <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
            </Structure>
            """
        );

        var data = LogixType.Deserialize(xml);

        data.Should().NotBeNull();
        data.Should().BeOfType<TIMER>();
    }

    [Test]
    public void TryCreate_NonRegistered_ShouldBeFalse()
    {
        var result = LogixType.TryCreate("Fake", out var data);

        result.Should().BeFalse();
        data.Should().BeNull();
    }

    [Test]
    [TestCase(typeof(BOOL))]
    [TestCase(typeof(SINT))]
    [TestCase(typeof(INT))]
    [TestCase(typeof(DINT))]
    [TestCase(typeof(LINT))]
    [TestCase(typeof(USINT))]
    [TestCase(typeof(UINT))]
    [TestCase(typeof(UDINT))]
    [TestCase(typeof(ULINT))]
    [TestCase(typeof(REAL))]
    [TestCase(typeof(LREAL))]
    [TestCase(typeof(DT))]
    [TestCase(typeof(LDT))]
    [TestCase(typeof(TIME))]
    [TestCase(typeof(TIME32))]
    [TestCase(typeof(LTIME))]
    [TestCase(typeof(TIMER))]
    public void IsRegistered_Atomics_ShouldBeExpected(Type type)
    {
        var result = LogixType.IsRegistered(type);

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_RegisteredType_ShouldBeTrue()
    {
        var result = LogixType.IsRegistered("TIMER");

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_FakeType_ShouldBeFalse()
    {
        var result = LogixType.IsRegistered("FAKE");

        result.Should().BeFalse();
    }

    [Test]
    [TestCase("BOOL")]
    [TestCase("SINT")]
    [TestCase("INT")]
    [TestCase("DINT")]
    [TestCase("LINT")]
    [TestCase("USINT")]
    [TestCase("UINT")]
    [TestCase("UDINT")]
    [TestCase("ULINT")]
    [TestCase("REAL")]
    [TestCase("LREAL")]
    [TestCase("DT")]
    [TestCase("LDT")]
    [TestCase("TIME")]
    [TestCase("TIME32")]
    [TestCase("LTIME")]
    public void IsAtomic_AtomicType_ShouldBeTrue(string typeName)
    {
        var result = LogixType.IsAtomic(typeName);

        result.Should().BeTrue();
    }


    [Test]
    public void IsAtomic_StructureType_ShouldBeFalse()
    {
        var result = LogixType.IsAtomic(nameof(TIMER));

        result.Should().BeFalse();
    }

    [Test]
    [TestCase(typeof(BOOL), "BOOL")]
    [TestCase(typeof(SINT), "SINT")]
    [TestCase(typeof(INT), "INT")]
    [TestCase(typeof(DINT), "DINT")]
    [TestCase(typeof(LINT), "LINT")]
    [TestCase(typeof(USINT), "USINT")]
    [TestCase(typeof(UINT), "UINT")]
    [TestCase(typeof(UDINT), "UDINT")]
    [TestCase(typeof(ULINT), "ULINT")]
    [TestCase(typeof(REAL), "REAL")]
    [TestCase(typeof(LREAL), "LREAL")]
    [TestCase(typeof(DT), "DT")]
    [TestCase(typeof(LDT), "LDT")]
    [TestCase(typeof(TIME), "TIME")]
    [TestCase(typeof(TIME32), "TIME32")]
    [TestCase(typeof(LTIME), "LTIME")]
    [TestCase(typeof(TIMER), "TIMER")]
    public void NameFor_ValidType_ShouldBeExpected(Type type, string name)
    {
        var result = LogixType.NameFor(type);

        result.Should().Be(name);
    }
}