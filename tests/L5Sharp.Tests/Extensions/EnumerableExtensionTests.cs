using FluentAssertions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Extensions;

[TestFixture]
public class EnumerableExtensionTests
{
    
    [Test]
    public void LookupTags_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var logic = content.Logic();

        var lookup = logic.ToTagLookup();

        lookup.Should().NotBeEmpty();
    }

    [Test]
    public void LookupTags_FindModuleTags_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var moduleTags = content.Modules().SelectMany(m => m.Tags()).ToList();
        
        var logic = content.Logic();

        var lookup = logic.ToTagLookup()
            .Where(t => moduleTags.Any(x => TagNameComparer.BaseName.Equals(x.TagName, t.Key)))
            .ToList();

        lookup.Should().NotBeEmpty();
    }
}