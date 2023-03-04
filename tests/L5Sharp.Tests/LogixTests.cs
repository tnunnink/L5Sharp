using FluentAssertions;
using L5Sharp.Types.Atomics;

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
}