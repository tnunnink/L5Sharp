using System.Net;
using FluentAssertions;

// ReSharper disable StringLiteralTypo
// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Core.Components;

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
        module.Vendor.Should().Be(0);
        module.ProductType.Should().Be(0);
        module.ProductCode.Should().Be(0);
        module.ParentModule.Should().BeEmpty();
        module.ParentModPortId.Should().Be(0);
        module.Inhibited.Should().BeFalse();
        module.SafetyEnabled.Should().BeFalse();
        module.MajorFault.Should().BeFalse();
        module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
        module.Ports.Should().NotBeNull();
        module.Connections.Should().BeEmpty();
        module.Config.Should().NotBeNull();
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
            Vendor = 1,
            ProductType = 10,
            ProductCode = 1,
            ParentModule = "Local",
            ParentModPortId = 1,
            Inhibited = true,
            SafetyEnabled = true,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled
        };

        module.Ports.Add(new Port { Id = 1, Type = "ICP", Address = "1", Upstream = true });

        module.Should().NotBeNull();
        module.Name.Should().Be("Test");
        module.Description.Should().Be("This is a test module");
        module.CatalogNumber.Should().Be("ABCD-1234");
        module.Revision.Should().Be("1.3");
        module.Vendor.Should().Be(1);
        module.ProductType.Should().Be(10);
        module.ProductCode.Should().Be(1);
        module.ParentModule.Should().Be("Local");
        module.ParentModPortId.Should().Be(1);
        module.Inhibited.Should().Be(true);
        module.SafetyEnabled.Should().Be(true);
        module.MajorFault.Should().Be(true);
        module.Keying.Should().Be(ElectronicKeying.Disabled);
    }

    [Test]
    public void IP_SetValue_ShouldBeUpdated()
    {
        var module = new Module("Test");
        module.Ports.Add(new Port { Id = 1, Type = "Ethernet", Upstream = true });

        module.IP = IPAddress.Loopback;

        module.IP.ToString().Should().Be("127.0.0.1");
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
            Vendor = 1,
            ProductType = 10,
            ProductCode = 1,
            ParentModule = "Local",
            ParentModPortId = 1,
            Inhibited = true,
            SafetyEnabled = true,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled
        };

        module.Ports.Add(new Port { Id = 1, Type = "ICP", Address = "1", Upstream = true });

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }
}