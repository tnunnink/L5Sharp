using FluentAssertions;


namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XTagTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tags.ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void AllTagsToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tags.SelectMany(t => t.Members()).ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void References_ForAKnownReferencedTag_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var tag = content.Tags.Get(Known.Tag);

        var references = tag.References();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void Scope_AllNestedTagMembers_ShouldNotHaveDoubleSlash()
    {
        var content = L5X.Load(Known.Test);

        var scopes = content.Query<Tag>().SelectMany(t => t.Members()).Select(t => t.Scope).ToList();

        scopes.Should().AllSatisfy(s => s.Path.Should().NotContain("//"));
    }

    [Test]
    public void GetRackConnectionAliasTagsShouldReturnExpectedElements()
    {
        var content = L5X.Load(Known.ModuleRackIoExport);

        var inAliasTag = content.Query<Tag>().FirstOrDefault(t => t.Name == "RackIO:1:I");

        inAliasTag.Should().NotBeNull();
        inAliasTag?.Value.Should().NotBeNull();
        
        var member = inAliasTag?.Member("Data");
        member.Should().NotBeNull();
        member?.Value.Should().Be(0);
    }

    [Test]
    public Task SetValue_RackConnectionAliasTag_ShouldUpdateTheParentModuleTagElement()
    {
        var content = L5X.Load(Known.ModuleRackIoExport);
        var member = content.Query<Tag>().First(t => t.Name == "RackIO:1:I")["Data"];
        
        member.Value = 1234;

        return Verify(content.Serialize().ToString());
    }
}