using FluentAssertions;
using L5Sharp.Catalog;
using L5Sharp.Core;
using L5Sharp.Tests.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Catalog;

[TestFixture]
public class ModuleDefinitionTests
{
    [Test]
    public void Generate_ValidInstance_ShouldNotBeNull()
    {
        var content = TestContent.Test;
        var module = content.Get<Module>(Known.Module);

        var definition = ModuleDefinition.Generate(module);

        definition.Should().NotBeNull();
    }

    [Test]
    public void Generate_KnownInstance_ShouldHaveExpectedValues()
    {
        var content = TestContent.Test;
        var module = content.Get<Module>(Known.Module);

        var definition = ModuleDefinition.Generate(module);

        definition.CatalogNumber.Should().Be(module.CatalogNumber);
        definition.Revision.Should().Be(module.Revision);
        definition.Vendor.Should().Be(module.Vendor);
        definition.ProductType.Should().Be(module.ProductType);
        definition.ProductCode.Should().Be(module.ProductCode);
        definition.Ports.Should().HaveCount(module.Ports.Count);
    }

    [Test]
    public void Generate_ModuleWithConnections_ShouldHaveExpectedValues()
    {
        var content = TestContent.Test;
        var module = content.Get<Module>("L1M1D1");

        var definition = ModuleDefinition.Generate(module);

        definition.CatalogNumber.Should().Be(module.CatalogNumber);
        definition.Revision.Should().Be(module.Revision);
        definition.Vendor.Should().Be(module.Vendor);
        definition.ProductType.Should().Be(module.ProductType);
        definition.ProductCode.Should().Be(module.ProductCode);
        definition.Ports.Should().HaveCount(module.Ports.Count);
    }

    [Test]
    public Task Serialize_KnownModule_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var module = content.Get<Module>(Known.Module);

        var definition = ModuleDefinition.Generate(module);

        return VerifyXml(definition.ToString());
    }

    [Test]
    public Task Serialize_ModuleWithConnections_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var module = content.Get<Module>("L1M1D1");

        var definition = ModuleDefinition.Generate(module);

        return VerifyXml(definition.ToString());
    }

    [Test]
    public Task Create_FromKnownDefinition_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var template = content.Get<Module>(Known.Module);
        var definition = ModuleDefinition.Generate(template);

        var module = definition.Create("Test");

        return VerifyXml(module.Serialize().ToString());
    }

    [Test]
    public Task Create_FromModuleWithConnections_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var template = content.Get<Module>("L1M1D1");
        var definition = ModuleDefinition.Generate(template);

        var module = definition.Create("Test");

        return VerifyXml(module.Serialize().ToString());
    }
}