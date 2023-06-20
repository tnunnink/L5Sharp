using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class MemberTests
{
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var member = new Member("Test", new BOOL());

        member.Should().NotBeNull();
    }

    [Test]
    public void New_ValidArguments_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", new BOOL());

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<BOOL>();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new Member(null!, new BOOL())).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_EmptyName_ShouldHaveEmptyName()
    {
        FluentActions.Invoking(() => new Member(string.Empty, new BOOL())).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullType_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new Member("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullLogixType_ShouldThrowNotSupportedException()
    {
        FluentActions.Invoking(() => new Member("Test", LogixType.Null)).Should().Throw<NotSupportedException>();
    }

    [Test]
    public void GetDataType_AtomicType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", 123);

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<DINT>();
        dataType.As<DINT>().Should().Be(123);
    }

    [Test]
    public void GetDataType_StructureType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", new TIMER { PRE = 123 });

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<TIMER>();
        dataType.As<TIMER>()?.PRE.Should().Be(123);
    }

    [Test]
    public void GetDataType_StringType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", "This is a test");

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<STRING>();
        dataType.As<STRING>()?.Should<STRING>().Be("This is a test");
    }

    [Test]
    public void SetDataType_AtomicType_ShouldBeExpectedValue()
    {
        var member = new Member("Test", 123);

        member.DataType = 321;

        member.DataType.As<DINT>().Should().Be(321);
    }

    [Test]
    public void SetDataType_StructureAsConcreteType_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.DataType = new TIMER { PRE = 5000, EN = true };

        member.DataType.To<TIMER>().PRE.Should().Be(5000);
        member.DataType.To<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsGenericStructureType_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.DataType = new StructureType("TIMER", new List<Member> { new("PRE", 5000), new("EN", true) });

        member.DataType.To<TIMER>().PRE.Should().Be(5000);
        member.DataType.To<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsDictionary_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "PRE", 5000 },
            { "EN", true }
        };

        member.DataType.To<TIMER>().PRE.Should().Be(5000);
        member.DataType.To<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureWithMembersNotInType_ShouldOnlySetMembersThatAreInType()
    {
        var member = new Member("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "Test", 123 },
            { "PRE", 5000 },
            { "Array", new DINT[] { 1, 2, 3, 4 } },
            { "EN", true}
        };
        
        member.DataType.To<TIMER>().PRE.Should().Be(5000);
        member.DataType.To<TIMER>().ACC.Should().Be(0);
        member.DataType.To<TIMER>().EN.Should().Be(true);
        member.DataType.To<TIMER>().TT.Should().Be(false);
        member.DataType.To<TIMER>().DN.Should().Be(false);
    }
}