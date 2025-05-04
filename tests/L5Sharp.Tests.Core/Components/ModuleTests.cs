using System.Net;
using FluentAssertions;

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
        module.Vendor.Should().Be(Vendor.Rockwell);
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
            Ports = [new Port { Id = 1, Type = "ICP", Address = "1", Upstream = true }],
        };

        module.Should().NotBeNull();
        module.Name.Should().Be("Test");
        module.Description.Should().Be("This is a test module");
        module.CatalogNumber.Should().Be("ABCD-1234");
        module.Revision.Should().Be("1.3");
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
    public Task Create_ValidNameAndCatalogNumber_ShouldBeVerified()
    {
        var module = Module.Create("Test", "1756-EN2T");

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Create_CatalogNumberAndConfig_ShouldBeVerified()
    {
        var module = Module.Create("1783-ETAP", m =>
        {
            m.Name = "MyModule";
            m.IP = IPAddress.Any;
            m.Revision = "1.2";
            m.Description = "This is a test of the create factory method using the module catalog.";
            m.Keying = ElectronicKeying.ExactMatch;
        });

        return Verify(module.Serialize().ToString());
    }

    [Test]
    public Task Create_ExistingValidCatalogNumber_ShouldBeVerified()
    {
        var template = Known.ModuleExport;

        var module = Module.Create(template, "1756-EN2T", m =>
        {
            m.Name = "NewName";
            m.Revision = 1.23;
            m.Description = "This is a new module instance based on an existing template";
            m.Inhibited = true;
        });

        return Verify(module.Serialize().ToString());
    }

    [Test]
    public void Create_InvalidCatalogNumber_ShouldThrowInvalidOperationException()
    {
        var action = () => Module.Create("1234-ABCD", m =>
        {
            m.Name = "MyCard";
            m.Description = "This will fail";
        });

        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public Task Local_ValidCatalogNumber_ShouldBeVerified()
    {
        var module = Module.Local("1756-L83E", "33.12");

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void IP_SetValue_ShouldBeUpdated()
    {
        var module = new Module
        {
            Name = "Test",
            Ports = [new Port { Id = 1, Type = "Ethernet", Upstream = true }]
        };

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
            Vendor = Vendor.Rockwell,
            ProductType = ProductType.Analog,
            ProductCode = 1,
            ParentModule = "Local",
            ParentModPortId = 1,
            Inhibited = true,
            SafetyEnabled = true,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled,
            Ports = [new Port { Id = 1, Type = "ICP", Address = "1", Upstream = true }]
        };

        var xml = module.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void Connect_ValidChildModule_ShouldHaveExpectedParentProperties()
    {
        var module = Module.Create("1756-EN2T", m =>
        {
            m.Name = "TestCard";
            m.IP = IPAddress.Loopback;
        });

        var child = module.Connect("1756-IF8", m => { m.Name = "ChildCard"; });

        child.ParentModule.Should().Be(module.Name);
        child.ParentModPortId.Should().Be(1);
        child.Ports.Should().Contain(p => p.Upstream && p.Type == "ICP");
    }

    [Test]
    public void Connect_ChildWithNotMatchingPort_ShouldThrowException()
    {
        var module = Module.Create("1756-EN2T", m =>
        {
            m.Name = "TestCard";
            m.IP = IPAddress.Loopback;
        });

        var action = () => module.Connect("5094-OF8", m => { m.Name = "ChildCard"; });

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("No matching downstream port type is available for 'TestCard'.");
    }

    [Test]
    public void Connect_ChildWithMultipleMatchingPort_ShouldThrowException()
    {
        var module = Module.Create("1756-EN2T", m =>
        {
            m.Name = "TestCard";
            m.IP = IPAddress.Loopback;
        });

        var action = () => module.Connect("1756-EN2T", m => { m.Name = "ChildCard"; });

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Multiple matching downstream ports types are available for on 'TestCard'.");
    }
}