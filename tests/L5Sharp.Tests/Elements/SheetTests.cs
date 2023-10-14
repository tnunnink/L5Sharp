using FluentAssertions;
using L5Sharp.Elements;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class SheetTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var sheet = new Sheet();

        sheet.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldNotBeAttached()
    {
        var sheet = new Sheet();

        sheet.IsAttached.Should().BeFalse();
        sheet.L5X.Should().BeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var sheet = new Sheet();

        sheet.Number.Should().Be(0);
        sheet.Description.Should().BeNull();
        sheet.ScopeName.Should().BeEmpty();
        sheet.Routine.Should().BeEmpty();
        sheet.InputReferences.Should().BeEmpty();
        sheet.OutputReferences.Should().BeEmpty();
        sheet.InputConnectors.Should().BeEmpty();
        sheet.OutputConnectors.Should().BeEmpty();
        sheet.Blocks.Should().BeEmpty();
        sheet.Functions.Should().BeEmpty();
        sheet.AddOnInstructions.Should().BeEmpty();
        sheet.JumpRoutines.Should().BeEmpty();
        sheet.SubRoutines.Should().BeEmpty();
        sheet.Returns.Should().BeEmpty();
        sheet.Wires.Should().BeEmpty();
        sheet.TextBoxes.Should().BeEmpty();
        sheet.Attachments.Should().BeEmpty();
    }

    [Test]
    public void InputReferences_AddValidReference_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        var reference = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "Test", HideDesc = true};
        
        sheet.InputReferences.Add(reference);

        sheet.InputReferences.Should().HaveCount(1);
    }
    
    [Test]
    public Task InputReferences_AddValidReference_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var reference = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "Test", HideDesc = true};
        
        sheet.InputReferences.Add(reference);

        return Verify(sheet.Serialize().ToString());
    }
    
    [Test]
    public Task InputReferences_AddAfterFirstElement_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var first = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "First", HideDesc = true};
        var second = new DiagramReference {ID = 1, X = 150, Y = 150, Operand = "Second"};
        sheet.InputReferences.Add(first);
        
        first.AddAfter(second);

        return Verify(sheet.Serialize().ToString());
    }
    
    [Test]
    public void OutputReferences_AddValidReference_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        var reference = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "Test", HideDesc = true};
        
        sheet.OutputReferences.Add(reference);

        sheet.OutputReferences.Should().HaveCount(1);
    }
    
    [Test]
    public Task OutputReferences_AddValidReference_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var reference = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "Test", HideDesc = true};
        
        sheet.OutputReferences.Add(reference);

        return Verify(sheet.Serialize().ToString());
    }
    
    [Test]
    public Task OutputReferences_AddAfterFirstElement_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var first = new DiagramReference {ID = 0, X = 100, Y = 100, Operand = "First", HideDesc = true};
        var second = new DiagramReference {ID = 1, X = 150, Y = 150, Operand = "Second"};
        sheet.OutputReferences.Add(first);
        
        first.AddAfter(second);

        return Verify(sheet.Serialize().ToString());
    }
    
    [Test]
    public void InputConnectors_AddValidConnector_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        var reference = new DiagramConnector {ID = 0, X = 100, Y = 100, Name = "Test"};
        
        sheet.InputConnectors.Add(reference);

        sheet.InputConnectors.Should().HaveCount(1);
    }
    
    [Test]
    public Task InputConnectors_AddValidConnector_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var reference = new DiagramConnector {ID = 0, X = 100, Y = 100, Name = "Test"};
        
        sheet.InputConnectors.Add(reference);

        return Verify(sheet.Serialize().ToString());
    }
    
    [Test]
    public Task InputConnectors_AddAfterConnector_ShouldBeVerified()
    {
        var sheet = new Sheet();
        var first = new DiagramConnector {ID = 0, X = 100, Y = 100, Name = "First"};
        var second = new DiagramConnector {ID = 1, X = 150, Y = 150, Name = "Second"};
        sheet.InputConnectors.Add(first);
        
        first.AddAfter(second);

        return Verify(sheet.Serialize().ToString());
    }
}