using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class ModuleCategoryTests
{
    [Test]
    public void Analog()
    {
        var type = ModuleCategory.Analog;

        type.Should().NotBeNull();
    }
}