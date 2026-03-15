using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XTagTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var result = content.Tags.ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void AllTagsToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var result = content.Tags.SelectMany(t => t.Members()).ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void Usages_ForAKnownReferencedTag_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;
        var tag = content.Tags.Get(Known.Tag);

        var references = tag.References();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void GetRackConnectionAliasTagsShouldReturnExpectedElements()
    {
        var content = TestContent.Load(TestFiles.Modules.RackIo);

        var inAliasTag = content.Query<Tag>().FirstOrDefault(t => t.Name == "RackIO:1:I");

        inAliasTag.Should().NotBeNull();
        inAliasTag.Value.Should().NotBeNull();

        var member = inAliasTag.Member("Data");
        member.Should().NotBeNull();
        member.Value.Should().Be(0);
    }

    [Test]
    public Task SetValue_RackConnectionAliasTag_ShouldUpdateTheParentModuleTagElement()
    {
        var content = TestContent.Load(TestFiles.Modules.RackIo);
        var member = content.Query<Tag>().First(t => t.Name == "RackIO:1:I")["Data"];

        member.Value = 1234;

        return VerifyXml(content.ToString())
            .ScrubMembersWithType<DateTime>()
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate");
    }
}