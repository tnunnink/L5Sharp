using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XReferencesTests
{
    #region TestFile

    [Test]
    public void References_FromKnownTagInstanceWithUsagesShouldNotBeEmpty()
    {
        var content = TestContent.Test;
        var tag = content.Get<Tag>(Known.Tag);

        var usages = tag.References().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void References_KnownDataType_ShouldReturnElementsWithExpectedDataType()
    {
        var content = TestContent.Test;
        var dataType = content.DataTypes.Get(Known.DataType);

        var usages = dataType.References().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void References_KnownInstruction_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;
        var instruction = content.Get<AddOnInstruction>(Known.AddOnInstruction);

        var usages = instruction.References().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void References_AllDataTypes_DoesThatWork()
    {
        var content = TestContent.Test;

        var usages = content.DataTypes.Select(d => new { d.Name, References = d.References().ToList() }).ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void References_AllTags_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var usages = content.Query<Tag>().Select(t => new { t.TagName, Refernces = t.References().ToList() })
            .ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void References_AllRoutines_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var usages = content.Query<Routine>()
            .Where(r => r.Program is not null)
            .Select(r => r.References())
            .ToList();

        usages.Should().NotBeEmpty();
    }

    #endregion

    #region ExampleFile

    [Test]
    public void References_ExampleDataType_ShouldHaveNoUnused()
    {
        var content = TestContent.Test;

        var unused = content.Query<DataType>()
            .Select(d => new { d.Name, References = d.References().ToList() })
            .Where(d => d.References.Count == 0)
            .ToList();

        unused.Should().NotBeEmpty();
    }

    [Test]
    public void References_ExampleAgainstAllTags_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var tags = content.Query<Tag>().ToList();

        var usages = tags.Select(t => new { t.TagName, Refernces = t.References() }).ToList();

        usages.Should().NotBeEmpty();
    }

    #endregion
}