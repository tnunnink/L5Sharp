using FluentAssertions;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixTests
{
    [Test]
    public void Module_ValidParameters_ShouldReturnExpectedModule()
    {
        var module = Logix.Module("Test", "1756-EN2T");

        module.Should().NotBeNull();
        module.CatalogNumber.Should().Be("1756-EN2T");
    }

    [Test]
    public void Array_ValidDimensions_ShouldReturnInitializedArray()
    {
        var array = Logix.Array<BOOL>(10);

        foreach (var element in array)
        {
            element.Should().NotBeNull();
        }
    }

    [Test]
    public void Tag_ValidParameters_ShouldBeExpected()
    {
        var tag = Logix.Tag<TIMER>("MyTimer");

        tag.Name.Should().Be("MyTimer");
        tag.Data.Should().BeOfType<TIMER>();
    }
}