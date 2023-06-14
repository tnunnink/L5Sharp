using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class DataTypeMemberTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var member = new DataTypeMember();

        member.Name.Should().BeEmpty();
        member.Description.Should().BeEmpty();
        member.DataType.Should().BeEmpty();
        member.Dimension.Should().BeEquivalentTo(Dimensions.Empty);
        member.Radix.Should().Be(Radix.Null);
        member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
    }

    [Test]
    public void DataTypeMember_ConstructorWithParameters_ShouldSetPropertiesToSpecifiedValues()
    {
        var name = "member1";
        var description = "description1";
        var dataType = "INT";
        var dimension = new Dimensions(10);
        var radix = Radix.Decimal;
        var externalAccess = ExternalAccess.ReadOnly;
        
        var member = new DataTypeMember
        {
            Name = name,
            Description = description,
            DataType = dataType,
            Dimension = dimension,
            Radix = radix,
            ExternalAccess = externalAccess
        };

        
        member.Name.Should().Be(name);
        member.Description.Should().Be(description);
        member.DataType.Should().Be(dataType);
        member.Dimension.Should().BeEquivalentTo(dimension);
        member.Radix.Should().Be(radix);
        member.ExternalAccess.Should().Be(externalAccess);
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
}