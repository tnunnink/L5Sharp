using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Types;

[TestFixture]
public class MemberTests
{
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var member = new LogixMember("Test", new BOOL());

        member.Should().NotBeNull();
    }

    [Test]
    public void New_ValidArguments_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new BOOL());

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<BOOL>();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new LogixMember(null!, new BOOL())).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullType_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new LogixMember("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void GetDataType_AtomicType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", 123);

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<DINT>();
        dataType.Should().Be(123);
    }

    [Test]
    public void GetDataType_StructureType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new TIMER { PRE = 123 });

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<TIMER>();
        dataType.As<TIMER>().PRE.Should().Be(123);
    }

    [Test]
    public void GetDataType_ArrayType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new DINT[] { 1, 2, 3, 4 });

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<ArrayType>();
        dataType.As<ArrayType>()[0].Should().Be(1);
        dataType.As<ArrayType>()[1].Should().Be(2);
        dataType.As<ArrayType>()[2].Should().Be(3);
        dataType.As<ArrayType>()[3].Should().Be(4);
    }

    [Test]
    public void GetDataType_StringType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", "This is a test");

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<STRING>();
        dataType.Should().Be("This is a test");
    }

    [Test]
    public void SetDataType_AtomicType_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", 123);

        member.DataType = 321;

        member.DataType.Should().Be(321);
    }

    [Test]
    public void SetDataType_StructureAsConcreteType_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new TIMER { PRE = 5000, EN = true };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsGenericStructureType_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new ComplexType("TIMER", new List<LogixMember> { new("PRE", 5000), new("EN", true) });

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsDictionary_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "PRE", 5000 },
            { "EN", true }
        };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureWithMembersNotInType_ShouldOnlySetMembersThatAreInType()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "Test", 123 },
            { "PRE", 5000 },
            { "Array", new DINT[] { 1, 2, 3, 4 } },
            { "EN", true }
        };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().ACC.Should().Be(0);
        member.DataType.As<TIMER>().EN.Should().Be(true);
        member.DataType.As<TIMER>().TT.Should().Be(false);
        member.DataType.As<TIMER>().DN.Should().Be(false);
    }

    [Test]
    public void SetDataType_ImmediateValue_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", 123);
        var monitor = member.Monitor();

        member.DataType = 321;

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public void SetDataType_NestedStructureValue_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", new TIMER());
        var monitor = member.Monitor();

        member.DataType = new TIMER { PRE = 5000 };

        monitor.Should().Raise("DataChanged");
    }
    
    [Test]
    public void SetDataType_AtomicBitMember_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", 123);
        var monitor = member.Monitor();

        member.DataType.As<DINT>()[0] = true;

        monitor.Should().Raise("DataChanged");
        member.DataType.Should().Be(1);
    }
}