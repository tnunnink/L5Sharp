using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentTemplateTests
{
    [Test]
    public void DataTypes_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.DataTypes().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Modules_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Modules().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Instructions_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Instructions().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Tags_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Tags().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Programs_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Programs().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Tasks_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Tasks().ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void InstructionsNames()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Instructions().Select(i => i.Name).ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void ProgramsWithNullType()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Programs().Where(p => p.Type == null).ToList();

        components.Should().BeEmpty();
    }


    [Test]
    public void FindReferencesToAnalogTag()
    {
        var content = LogixContent.Load(Known.Template);

        var tags = content.Tags().ToList();

        var lookup = new Dictionary<string, List<NeutralText>>();

        foreach (var tag in tags)
        {
            var references = content.Query<Rung>().Select(r => r.Text)
                .Where(t => t.ContainsTag(tag.TagName, TagNameComparer.BaseName)).ToList();

            lookup.Add(tag.TagName, references);
        }

        lookup.Should().NotBeEmpty();

        //flatten returns rungs somehow.
        //filter further based on perhaps predefined instruction set. (like find all MOV instruction with this tag name to find buffer tag)
    }

    [Test]
    public void FindReferencesDifferentApproach()
    {
        var content = LogixContent.Load(Known.Template);

        var references = content.Query<Rung>()
            .Select(r => r.Text)
            .SelectMany(t => t.References()).ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void FindInstructionAndReplaceWithSignature()
    {
        var content = LogixContent.Load(Known.Template);

        var aoiLookup = content.Instructions().ToDictionary(k => k.Name, v => v);

        var aoiReferences = content.Query<Rung>()
            .Select(r => r.Text)
            .SelectMany(t => aoiLookup.SelectMany(l => t.Split(l.Key)))
            .ToList();

        var flattened = new List<NeutralText>();

        foreach (var reference in aoiReferences)
        {
            var key = reference.Keys().First();
            var instruction = aoiLookup[key];
            var logic = instruction.GetLogic(reference);
            flattened.AddRange(logic);
        }

        flattened.Should().NotBeEmpty();

        /*var ioReferencesToTag = flattened.SelectMany(t => t.Split(Instruction.MOV))
            .Where(t => t.ContainsTag("IO_PLC_FLEX:4:I", TagNameComparer.BaseName))
            .ToList();*/

        /*File.WriteAllLines(@"C:\Users\tnunnink\Documents\Temp\IoRefs.txt", ioReferencesToTag.Select(t => t.ToString()));

        File.WriteAllLines(@"C:\Users\tnunnink\Documents\Temp\Flattened.txt", flattened.Select(f => f.ToString()));*/
    }
}