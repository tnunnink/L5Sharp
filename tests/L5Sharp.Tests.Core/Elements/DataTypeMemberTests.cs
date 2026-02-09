using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class DataTypeMemberTests
{
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
    public void TryGetDefinition_NotAttached_ShouldReturnFalse()
    {
        var member = new DataTypeMember();

        var result = member.TryGetDefinition(out _);

        result.Should().BeFalse();
    }

    [Test]
    public void TryGetDefinition_FromMemberInContext_ShouldReturnTrue()
    {
        var content = L5X.Load(Known.Test);
        var type = content.DataTypes.Get("ComplexType");
        var member = type.Members.First(m => m.DataType == "SimpleType");

        var result = member.TryGetDefinition(out var definition);

        result.Should().BeTrue();
        definition.Should().NotBeNull();
        definition.Name.Should().Be("SimpleType");
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
}