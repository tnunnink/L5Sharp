using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentAgainstTemplateTests
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

        var tag = content.Tags().Find("ai_01AT_101");

        var references = content.Query<Rung>().Select(r => r.Text)
            .Where(t => t.ContainsTag("ai_01AT_101", TagNameComparer.BaseName)).ToList();

        //flatten returns rungs somehow.
        //filter further based on perhaps predefined instruction set. (like find all MOV instruction with this tag name to find buffer tag)
    }
}