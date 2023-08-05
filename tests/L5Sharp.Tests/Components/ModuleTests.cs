using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Enums;
using Task = System.Threading.Tasks.Task;

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
        module.Revision.Should().Be("1.0");
        module.Vendor.Should().Be(Vendor.Unknown);
        module.ProductType.Should().Be(ProductType.Unknown);
        module.ProductCode.Should().Be(0);
        module.ParentModule.Should().BeEmpty();
        module.ParentModPortId.Should().Be(0);
        module.Inhibited.Should().BeFalse();
        module.SafetyEnabled.Should().BeFalse();
        module.MajorFault.Should().BeFalse();
        module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
        module.Ports.Should().NotBeNull();
        module.Communications?.ConfigTag.Should().NotBeNull();
    }

    [Test]
    public void New_Initialized_ShouldHaveExpectedValues()
    {
        var module = new Module
        {
            Name = "Test",
            Description = "This is a test module",
            CatalogNumber = "ABCD-1234",
            Revision = 1.3,
            Vendor = Vendor.Rockwell,
            ProductType = ProductType.Analog,
            ProductCode = 1,
            ParentModule = "Local",
            ParentModPortId = 1,
            Inhibited = true,
            SafetyEnabled = true,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled,
            Ports = new LogixContainer<Port> { new() { Id = 1, Type = "ICP", Address = "1", Upstream = true } },
        };

        module.Should().NotBeNull();
        module.Name.Should().Be("Test");
        module.Description.Should().Be("This is a test module");
        module.CatalogNumber.Should().Be("ABCD-1234");
        module.Revision.Should().Be( 1.3);
        module.Vendor.Should().Be(Vendor.Rockwell);
        module.ProductType.Should().Be(ProductType.Analog);
        module.ProductCode.Should().Be(1);
        module.ParentModule.Should().Be("Local");
        module.ParentModPortId.Should().Be(1);
        module.Inhibited.Should().Be(true);
        module.SafetyEnabled.Should().Be(true);
        module.MajorFault.Should().Be(true);
        module.Keying.Should().Be(ElectronicKeying.Disabled);
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var module = new Module();

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }
    
    [Test]
    public Task Serialize_Initialized_ShouldBeVerified()
    {
        var module = new Module
        {
            Name = "Test",
            Description = "This is a test module",
            CatalogNumber = "ABCD-1234",
            Revision = 1.3,
            Vendor = Vendor.Rockwell,
            ProductType = ProductType.Analog,
            ProductCode = 1,
            ParentModule = "Local",
            ParentModPortId = 1,
            Inhibited = true,
            SafetyEnabled = true,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled,
            Ports = new LogixContainer<Port> { new() { Id = 1, Type = "ICP", Address = "1", Upstream = true } }
        };

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }
}