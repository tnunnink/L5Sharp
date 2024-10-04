using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class L5XReferenceTests
{
    [Test]
    public void FindReferences_ComponentWithKnownReference_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        
        var references = content.References("TestSimpleTag").ToList();
        
        references.Should().NotBeEmpty();
    }
}