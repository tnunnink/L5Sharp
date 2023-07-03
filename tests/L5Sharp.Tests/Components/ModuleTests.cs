using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class ModuleTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var module = new Module();

        module.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var module = new Module();

        module.Name.Should().BeEmpty();
        module.Description.Should().BeNull();
        module.CatalogNumber.Should().BeEmpty();
        module.Revision.Should().Be(new Revision());
        module.Vendor.Should().Be(Vendor.Unknown);
        module.ProductType.Should().Be(ProductType.Unknown);
        module.ProductCode.Should().Be(0);
        module.ParentModule.Should().BeEmpty();
        module.ParentPortId.Should().Be(0);
        module.Inhibited.Should().BeFalse();
        module.SafetyEnabled.Should().BeFalse();
        module.MajorFault.Should().BeFalse();
        module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
        module.Ports.Should().NotBeNull();
        module.Config.Should().BeNull();
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var module = new Module();

        var xml = module.Serialize().ToString();
        
        return Verify(xml);
    }
}