using FluentAssertions;
using JetBrains.dotMemoryUnit;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixRegistryTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var registry = new LogixRegistry();

        registry.Should().NotBeNull();
    }

    [Test]
    public void Scan_KnownTest_ShouldHaveExpectedContent()
    {
        var registry = new LogixRegistry();

        var result = registry.Scan(Known.Test);

        result.Should().BeGreaterThan(0);
    }

    [Test]
    public void Scan_NonExistentFile_ShouldThrowFileNotFoundException()
    {
        var registry = new LogixRegistry();

        var action = () => registry.Scan("fake.L5X");

        action.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void Scan_MultipleTimesWithDefaultStrategy_ShouldThrowException()
    {
        var registry = new LogixRegistry();
        registry.Scan(Known.Test);

        var action = () => registry.Scan(Known.Test);

        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Scan_MultipleTimesWithKeepFirstStrategy_ShouldReturnZeroRegistered()
    {
        var registry = new LogixRegistry(ConflictStrategy.KeepFirst);
        registry.Scan(Known.Test);

        var result = registry.Scan(Known.Test);

        result.Should().Be(0);
    }

    [Test]
    public void Scan_MultipleTimesWithOverwriteExistingStrategy_ShouldReturnSameCountAsFirstRun()
    {
        var registry = new LogixRegistry(ConflictStrategy.OverwriteExisting);
        var expected = registry.Scan(Known.Test);

        var result = registry.Scan(Known.Test);

        result.Should().Be(expected);
    }

    [DotMemoryUnit(FailIfRunWithoutSupport = false)]
    [Test]
    public void Scan_ManyTimes_EnsureNoResidualObjectsInMemory()
    {
        var registry = new LogixRegistry(ConflictStrategy.OverwriteExisting);

        for (var i = 0; i < 100; i++)
        {
            var result = registry.Scan(Known.Test);
            result.Should().BeGreaterThan(0);
        }

        dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<L5X>()).ObjectsCount.Should().Be(0));
    }

#if NET7_0_OR_GREATER
    [Test]
    public void ScanAll_KnownDirectory_ShouldHaveNonZeroCount()
    {
        var registry = new LogixRegistry(ConflictStrategy.KeepFirst);

        var result = registry.ScanAll(Known.Directory);

        result.Should().BeGreaterThan(0);
    }
#endif

    [Test]
    public void ListTemplates_EmptyRegistry_ShouldBeEmpty()
    {
        var registry = new LogixRegistry();

        var templates = registry.ListTemplates<DataType>();

        templates.Should().BeEmpty();
    }
    
    [Test]
    public void ListTemplates_HasTemplates_ShouldNotBeEmpty()
    {
        var registry = new LogixRegistry();
        registry.Scan(Known.Test);

        var templates = registry.ListTemplates<DataType>().ToList();

        templates.Should().NotBeEmpty();
    }

    [Test]
    public void GetTemplate_EmptyRegistry_ShouldThrowException()
    {
        var registry = new LogixRegistry();

        var action = () => registry.GetTemplate<DataType>(Known.DataType);

        action.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void GetTemplate_HasTemplate_ShouldBeExpected()
    {
        var registry = new LogixRegistry();
        registry.Scan(Known.Test);

        var component = registry.GetTemplate<DataType>(Known.DataType);

        component.Should().NotBeNull();
        component.Name.Should().Be("SimpleType");
    }

    [Test]
    public void TryGetTemplate_EmptyRegistry_ShouldReturnFalse()
    {
        var registry = new LogixRegistry();

        var result = registry.TryGetTemplate<DataType>(Known.DataType, out _);

        result.Should().BeFalse();
    }

    [Test]
    public void TryGetTemplate_HasTemplate_ShouldReturnTrueAndExpected()
    {
        var registry = new LogixRegistry();
        registry.Scan(Known.Test);

        var result = registry.TryGetTemplate<DataType>(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be("SimpleType");
    }

    [Test]
    public Task CreateModule_ValidExistingCatalog_ShouldNotBeExpected()
    {
        var registry = new LogixRegistry();
        registry.Scan(Known.Test);

        var module = registry.CreateModule("1756-EN2T", m =>
        {
            m.Name = "NewModule";
            m.Description = "This is a test from the registry";
            m.Revision = 12.3;
            m.Inhibited = true;
        });

        return Verify(module.Serialize().ToString());
    }
}