using FluentAssertions;

namespace L5Sharp.Tests.Core.Entities;

[TestFixture]
public class DataTypeMemberTests
{
    [Test]
    public void New_NullTagName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new DataTypeMember(null!, "DINT")).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullDataType_ShouldThrowException()
    {
        FluentActions.Invoking(() => new DataTypeMember("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var member = new DataTypeMember { Name = "Test", DataType = "BOOL" };

        member.Name.Should().Be("Test");
        member.Description.Should().BeNull();
        member.DataType.Should().Be("BOOL");
        member.Dimension.Should().BeEquivalentTo(Dimensions.Empty);
        member.Radix.Should().Be(Radix.Null);
        member.ExternalAccess.Should().Be(Access.ReadWrite);
        member.Parent.Should().BeNull();
    }

    [Test]
    public void New_OverloadedProperties_ShouldHaveExpectedValues()
    {
        var member = new DataTypeMember
        {
            Name = "Test",
            Description = "This is a test",
            DataType = "INT",
            Dimension = 10,
            Radix = Radix.Decimal,
            ExternalAccess = Access.ReadOnly
        };


        member.Name.Should().Be("Test");
        member.Description.Should().Be("This is a test");
        member.DataType.Should().Be("INT");
        member.Dimension.Should().Be(new Dimensions(10));
        member.Radix.Should().Be(Radix.Decimal);
        member.ExternalAccess.Should().Be(Access.ReadOnly);
    }

    [Test]
    public void Parent_WhenAddedToDataType_ShouldBeExpected()
    {
        var type = new DataType("test");
        var member = new DataTypeMember("Member", "DINT");
        type.Members.Add(member);

        var parent = member.Parent;

        parent.Should().NotBeNull();
        parent.Name.Should().Be("test");
        parent.Members.Should().HaveCount(1);
    }

    [Test]
    public void Clone_WhenCalled_ShouldReturnExpectedType()
    {
        var member = new DataTypeMember
        {
            Name = "Test",
            DataType = "REAL",
            Dimension = new Dimensions(3),
            Radix = Radix.Exponential,
            ExternalAccess = Access.ReadWrite,
            Description = "This is a test",
            Hidden = true,
            Target = "SomeOtherMember",
            BitNumber = 12
        };

        var clone = member.Clone();

        clone.Should().BeOfType<DataTypeMember>();
        clone.Should().NotBeSameAs(member);
        clone.Name.Should().Be(member.Name);
        clone.DataType.Should().Be(member.DataType);
        clone.Dimension.Should().Be(member.Dimension);
        clone.Radix.Should().Be(member.Radix);
        clone.ExternalAccess.Should().Be(member.ExternalAccess);
        clone.Description.Should().Be(member.Description);
        clone.Hidden.Should().Be(member.Hidden);
        clone.Target.Should().Be(member.Target);
        clone.BitNumber.Should().Be(member.BitNumber);
    }

    [Test]
    public Task Serialize_WhenCalled_ShouldBeVerified()
    {
        var member = new DataTypeMember();

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void ToMember_AtomicData_ShouldBeExpectedValues()
    {
        var member = new DataTypeMember
        {
            Name = "Test",
            DataType = "REAL",
            Dimension = new Dimensions(3),
            Radix = Radix.Exponential,
            ExternalAccess = Access.ReadWrite,
            Description = "This is a test",
            Hidden = true,
            Target = "SomeOtherMember",
            BitNumber = 12
        };

        var instance = member.ToMember();

        instance.Name.Should().Be("Test");
        instance.Value.Should().Be(new REAL());
    }
    [Test]
    public void ToMember_StructureData_ShouldBeExpectedValues()
    {
        var member = new DataTypeMember
        {
            Name = "Test",
            DataType = "TIMER",
        };

        var instance = member.ToMember();

        instance.Name.Should().Be("Test");
        instance.Value.Should().BeOfType<TIMER>();
    }

    [Test]
    public void New_NameAndDataType_ShouldHaveExpectedValues()
    {
        var member = new DataTypeMember("TestMember", "REAL");

        member.Name.Should().Be("TestMember");
        member.DataType.Should().Be("REAL");
        member.Dimension.Should().BeEquivalentTo(Dimensions.Empty);
    }

    [Test]
    public void Dimensions_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "ArrayMember",
            DataType = "DINT",
            Dimension = new Dimensions(10)
        };

        member.Dimension.Should().NotBeNull();
        member.Dimension.Length.Should().Be(10);
    }

    [Test]
    public void Radix_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "HexMember",
            DataType = "DINT",
            Radix = Radix.Hex
        };

        member.Radix.Should().Be(Radix.Hex);
    }

    [Test]
    public void ExternalAccess_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "ReadOnlyMember",
            DataType = "DINT",
            ExternalAccess = Access.ReadOnly
        };

        member.ExternalAccess.Should().Be(Access.ReadOnly);
    }

    [Test]
    public void Description_WhenNull_ShouldBeNull()
    {
        var member = new DataTypeMember { Name = "Test", DataType = "BOOL", Description = null };

        member.Description.Should().BeNull();
    }

    [Test]
    public void Hidden_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "HiddenMember",
            DataType = "DINT",
            Hidden = true
        };

        member.Hidden.Should().BeTrue();
    }

    [Test]
    public void Target_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "TargetMember",
            DataType = "BOOL",
            Target = "OtherMember"
        };

        member.Target.Should().Be("OtherMember");
    }

    [Test]
    public void BitNumber_WhenSet_ShouldBeExpected()
    {
        var member = new DataTypeMember
        {
            Name = "BitMember",
            DataType = "BOOL",
            BitNumber = 5
        };

        member.BitNumber.Should().Be(5);
    }

    [Test]
    public void ToMember_ArrayData_ShouldBeExpectedValues()
    {
        var member = new DataTypeMember
        {
            Name = "ArrayMember",
            DataType = "INT",
            Dimension = new Dimensions(5)
        };

        var instance = member.ToMember();

        instance.Name.Should().Be("ArrayMember");
        instance.Value.Should().BeOfType<ArrayData>();
    }

    [Test]
    public Task Serialize_WithAllProperties_ShouldBeVerified()
    {
        var member = new DataTypeMember
        {
            Name = "FullMember",
            Description = "Complete test member",
            DataType = "DINT",
            Dimension = new Dimensions(3),
            Radix = Radix.Hex,
            ExternalAccess = Access.ReadOnly,
            Hidden = true,
            Target = "TargetMember",
            BitNumber = 7
        };

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }
}