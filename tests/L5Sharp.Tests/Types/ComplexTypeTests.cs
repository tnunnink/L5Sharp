using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class ComplexTypeTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var type = new ComplexType();

        type.Should().NotBeNull();
    }
    
    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var type = new ComplexType();

        type.Name.Should().Be(nameof(ComplexType));
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().BeEmpty();
    }
    
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var type = new ComplexType("Test", new List<Member>());

        type.Should().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ComplexType(null!, new List<Member>())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullMembers_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ComplexType("Test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_EmptyMembers_ShouldHaveExpectedValues()
    {
        var type = new ComplexType("Test", new List<Member>());

        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void New_WithMembers_ShouldHaveExpectedValues()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().HaveCount(5);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeName()
    {
        var type = new ComplexType("Test", new List<Member>());

        var name = type.ToString();

        name.Should().Be(type.Name);
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeSameAsButEqual()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });
        
        var clone = type.Clone();

        clone.Should().BeOfType<ComplexType>();
        clone.Should().NotBeSameAs(type);
        clone.Name.Should().Be(type.Name);
        clone.Members.Should().HaveCount(type.Members.Count());
    }

    [Test]
    public void Add_ValidMember_ShouldHaveExpectedCount()
    {
        var type = new ComplexType();
        
        type.Add(new Member("Member", 123));

        type.Members.Should().HaveCount(1);
    }
    
    [Test]
    public void Add_ValidMember_ShouldHaveExpectedMember()
    {
        var type = new ComplexType();
        
        type.Add(new Member("Member", 123));

        var result = type.Members.First();

        result.Name.Should().Be("Member");
        result.DataType.Should().BeOfType<DINT>();
        result.DataType.Should().Be(123);
    }

    [Test]
    public void Add_Null_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType();

        FluentActions.Invoking(() => type.Add(null!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void AddRange_ValidMembers_ShouldHaveExpectedCount()
    {
        var expected = new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER {PRE = 2000}),
        };
        var type = new ComplexType();
        
        type.AddRange(expected);

        type.Members.Should().HaveCount(3);
    }
    
    [Test]
    public void AddRange_ValidMembers_ShouldHaveExpectedMember()
    {
        var expected = new List<Member>
        {
            new("Atomic", 123),
            new("String", "Test Value"),
            new("Structure", new TIMER {PRE = 2000}),
        };
        var type = new ComplexType();
        
        type.AddRange(expected);

        var a = type.Member("Atomic");
        a?.Name.Should().Be("Atomic");
        a?.DataType.Should().BeOfType<DINT>();
        a?.DataType.Should().Be(123);
        
        var b = type.Member("String");
        b?.Name.Should().Be("String");
        b?.DataType.Should().BeOfType<STRING>();
        b?.DataType.Should().Be("Test Value");
        
        var c = type.Member("Structure");
        c?.Name.Should().Be("Structure");
        c?.DataType.Should().BeOfType<TIMER>();
        c?.DataType.As<TIMER>().PRE.Should().Be(2000);
    }

    [Test]
    public void AddRange_Null_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType();

        FluentActions.Invoking(() => type.AddRange(null!)).Should().Throw<ArgumentNullException>();
    }
}