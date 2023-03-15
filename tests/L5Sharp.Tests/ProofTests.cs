using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests;

[TestFixture]
public class ProofTests
{
    [Test]
    public void ValidateSomeTagValue()
    {
        var content = LogixContent.Load(Known.Test);

        var tags = content.Tags().Where(t => t.DataType == "DINT" && t.Data is AtomicType);

        foreach (var tag in tags)
        {
            tag.Data.ToType<DINT>().Should().BeGreaterOrEqualTo(0);
        }
    }

    [Test]
    public void UnusedTags()
    {
        var content = LogixContent.Load(Known.Test);

        var referencedTags = content.Logic().SelectMany(t => t.Tags());

        var unused = content.Tags().Select(t => t.TagName).Where(t => referencedTags.All(r => r != t)).ToList();

        foreach (var tagName in unused)
        {
            Console.WriteLine(tagName);
        }
    }

    [Test]
    public void ModuleTagComments()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Transfer\Site.L5X");

        var config = content.Modules().Find("R0S4").Config;

        var comment = string.Empty;
        
        config?.Comments.TryGetValue(".CH0CountLimit", out comment);

        comment.Should().NotBeEmpty();

        var member = config?.Member("Ch0CountLimit");

        member?.As<TagMember>().Comment.Should().NotBeEmpty();
    }

    /*[Test]
    public void FindReferencedModuleTags()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Transfer\Site.L5X");

        var tagMembers = content.Modules().SelectMany(m => m.Tags()).SelectMany(t => t.Members()).ToList();

        var logic = content.LogicFlatten().ToTagLookup();

        var references = logic.Where(k => tagMembers.Any(m => m.TagName == k.Key)).ToList();

        references.Should().NotBeEmpty();
    }*/
}