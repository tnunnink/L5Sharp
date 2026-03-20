using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XReferencesTests
{
    [Test]
    public void TagReferences_ProjectWithSimpleRungReferences_ShouldBeExpectedCount()
    {
        var project = L5X.New("MyProject", "1756-L84E", 34.1)
            .Add(Tag.Named("MyTag").WithValue(123).Build())
            .Add(Tag.Named("Program:MyProgram.LocalTag").WithValue<TIMER>(t => t.PRE = 5000).Build())
            .Add(Routine.Rll("MyRoutine").InProgram("MyProgram")
                .WithRung("XIC(MyTag)TON(LocalTag,?,?);")
                .WithRung("XIC(MyTag)OTE(MyTag);")
                .Build());

        var references = project.Tags.Get("MyTag").References().ToList();
        
        references.Should().HaveCount(3);
    }

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
        var content = TestContent.Example;

        var unused = content.Query<DataType>()
            .Select(d => new { d.Name, References = d.References().ToList() })
            .Where(d => d.References.Count == 0)
            .ToList();

        unused.Should().NotBeEmpty();
    }

    [Test]
    public void References_ExampleAgainstAllTags_ShouldNotBeEmpty()
    {
        var content = TestContent.Example;

        var tags = content.Query<Tag>().ToList();

        var usages = tags.Select(t => new { t.TagName, Refernces = t.References() }).ToList();

        usages.Should().NotBeEmpty();
    }

    #endregion
}