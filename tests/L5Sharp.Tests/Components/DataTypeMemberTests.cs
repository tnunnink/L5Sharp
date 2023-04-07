using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class DataTypeMemberTests
{
    [Test]
    public void DataTypeMember_DefaultConstructor_ShouldSetPropertiesToDefaultValues()
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
}