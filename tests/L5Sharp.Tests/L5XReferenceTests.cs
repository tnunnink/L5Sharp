using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Samples;

namespace L5Sharp.Tests;

[TestFixture]
public class L5XReferenceTests
{
    [Test]
    public void FindReferences_ComponentWithKnownReference_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);
        
        var references = content.ReferencesTo<Tag>("TestSimpleTag").ToList();
        
        references.Should().NotBeEmpty();
    }
    
    [Test]
    public void UpdatingTextForKnownRungWithTagReferenceShouldUpdateAndReturnEmptyReferences()
    {
        var content = L5X.Load(Known.Test);
        var initialReferences = content.ReferencesTo<Tag>("TestSimpleTag").ToList();
        initialReferences.Should().NotBeEmpty();
        var rung = initialReferences.First(r =>
                r.Container == "MainProgram" && r.Routine == "Main" && r.ElementId == "2")
            .Element as Rung;
        rung.Should().NotBeNull();

        //once we update the text we have replace the references to it.
        //It should update the index internally an recalling FindReferences should return an empty collection.
        rung!.Text = "This is me fucking with the text.";

        var finalReferences = content.ReferencesTo<Tag>("TestSimpleTag").ToList();
        finalReferences.Should().BeEmpty();
    }
}