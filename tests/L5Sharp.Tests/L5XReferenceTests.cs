using FluentAssertions;


namespace L5Sharp.Tests;

//todo
[TestFixture]
public class L5XReferenceTests
{
    [Test]
    public void FindReferences_ComponentWithKnownReference_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);
        
        var references = content.References("TestSimpleTag").ToList();
        
        references.Should().NotBeEmpty();
    }
    
    [Test]
    public void UpdatingTextForKnownRungWithTagReferenceShouldUpdateAndReturnEmptyReferences()
    {
        //todo 
        /*var content = L5X.Load(Known.Test);
        var initialReferences = content.References<Tag>("TestSimpleTag").ToList();
        initialReferences.Should().NotBeEmpty();
        var rung = initialReferences.First(r => r.Scope.Is())
        //once we update the text we have replace the references to it.
        //It should update the index internally an recalling FindReferences should return an empty collection.
        rung!.Text = "This is me fucking with the text.";

        var finalReferences = content.References<Tag>("TestSimpleTag").ToList();
        finalReferences.Should().BeEmpty();*/
    }
}