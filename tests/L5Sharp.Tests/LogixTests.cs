using FluentAssertions;

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
}