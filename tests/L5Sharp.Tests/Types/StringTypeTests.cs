using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class StringTypeTests
{
    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringType(null!, "")).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullValue_ShouldHaveEmptyString()
    {
        var type = new StringType("Test", null!);

        type.Should().BeEmpty(string.Empty);
    }

    [Test]
    public void New_EmptyString_ShouldNotBeNull()
    {
        var type = new StringType("Test", "");

        type.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var type = new StringType();

        type.Name.Should().Be(nameof(StringType));
        type.Family.Should().Be(DataTypeFamily.String);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().HaveCount(2);
        type.LEN.Should().Be(0);
        type.DATA.As<ArrayType>().Should().HaveCount(1);
    }

    [Test]
    public void METHOD()
    {
        var type = new StringType("This is a test");

        type.Member("DATA")!.DataType = new ArrayType<SINT>(new SINT[] { 1, 2, 3, 4 });

        type.DATA[1].Should().Be(2);
    }
}