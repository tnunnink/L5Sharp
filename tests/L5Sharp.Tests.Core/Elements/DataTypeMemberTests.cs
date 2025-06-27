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
        member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        member.Parent.Should().BeNull();
        member.Definition.Should().NotBeNull();
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
            ExternalAccess = ExternalAccess.ReadOnly
        };


        member.Name.Should().Be("Test");
        member.Description.Should().Be("This is a test");
        member.DataType.Should().Be("INT");
        member.Dimension.Should().Be(new Dimensions(10));
        member.Radix.Should().Be(Radix.Decimal);
        member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
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
            ExternalAccess = ExternalAccess.ReadWrite,
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
            ExternalAccess = ExternalAccess.ReadWrite,
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