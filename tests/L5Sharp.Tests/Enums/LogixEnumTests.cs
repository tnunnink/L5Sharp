using FluentAssertions;

namespace L5Sharp.Tests.Enums;

[TestFixture]
public class LogixEnumTests
{
    [Test]
    public void Names_NullType_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixEnum.Names(typeof(Rung))).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Names_InvalidType_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixEnum.Names(null!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void Options_NullType_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixEnum.Options(typeof(Rung))).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Options_InvalidType_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixEnum.Options(null!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void Names_ValidEnumType_ShouldReturnExpected()
    {
        var results = LogixEnum.Names<TagType>().ToList();

        results.Should().NotBeEmpty();
        results.Should().Contain("Base");
        results.Should().Contain("Alias");
        results.Should().Contain("Produced");
        results.Should().Contain("Consumed");
    }

    [Test]
    public void Names_NonGenericValidType_ShouldReturnExpected()
    {
        var names = LogixEnum.Names(typeof(TagType)).ToList();

        names.Should().NotBeEmpty();
        names.Should().Contain("Base");
        names.Should().Contain("Alias");
        names.Should().Contain("Produced");
        names.Should().Contain("Consumed");
    }

    [Test]
    public void Options_GenericValidType_ShouldReturnExpected()
    {
        var options = LogixEnum.Options<RoutineType>().ToList();

        options.Should().NotBeEmpty();
        options.Should().Contain(RoutineType.Typeless);
        options.Should().Contain(RoutineType.RLL);
        options.Should().Contain(RoutineType.ST);
        options.Should().Contain(RoutineType.FBD);
        options.Should().Contain(RoutineType.SFC);
    }

    [Test]
    public void Options_NonGenericValidType_ShouldReturnExpected()
    {
        var options = LogixEnum.Options(typeof(ExternalAccess)).ToList();

        options.Should().NotBeEmpty();
        options.Should().Contain(ExternalAccess.None);
        options.Should().Contain(ExternalAccess.ReadOnly);
        options.Should().Contain(ExternalAccess.ReadWrite);
    }
}