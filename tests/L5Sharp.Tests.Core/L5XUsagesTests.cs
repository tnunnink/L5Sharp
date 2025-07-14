using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XUsagesTests
{
    #region TestFile

    [Test]
    public void Usages_FromKnownTagInstanceWithUsagesShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var tag = content.Get<Tag>(Known.Tag);

        var usages = tag.Usages().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_KnownDataType_ShouldReturnElementsWithExpectedDataType()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var dataType = content.DataTypes.Get(Known.DataType);

        var usages = dataType.Usages().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_KnownInstruction_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var instruction = content.Get<AddOnInstruction>(Known.AddOnInstruction);

        var usages = instruction.Usages().ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_AllDataTypes_DoesThatWork()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var usages = content.DataTypes.Select(d => new { d.Name, References = d.Usages().ToList() }).ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_AllTags_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var usages = content.Query<Tag>().Select(t => new { t.TagName, Refernces = t.Usages().ToList() })
            .ToList();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_AllRoutines_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var usages = content.Query<Routine>()
            .Where(r => r.Program is not null)
            .Select(r => r.Usages())
            .ToList();

        usages.Should().NotBeEmpty();
    }

    #endregion

    #region ExampleFile

    [Test]
    public void Usages_ExampleDataType_ShouldHaveNoUnused()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var unused = content.Query<DataType>()
            .Select(d => new { d.Name, References = d.Usages().ToList() })
            .Where(d => d.References.Count == 0)
            .ToList();

        unused.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_ExampleAgainstAllTags_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var tags = content.Query<Tag>().ToList();

        var usages = tags.Select(t => new { t.TagName, Refernces = t.Usages() }).ToList();

        usages.Should().NotBeEmpty();
    }

    #endregion
}