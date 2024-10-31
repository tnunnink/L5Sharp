using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class CrossReferenceTests
{
    [Test]
    public void In_TagSimpleBoolSample_ShouldReturnExpectedCount()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleBool());

        var references = CrossReference.In(element).ToList();

        references.Should().HaveCount(1);
        references.Should().AllSatisfy(r => r.Reference.Should().NotBeNull());
        references.Should().AllSatisfy(r => r.Scope.Should().Be(Scope.Of(element)));
        references.Should().AllSatisfy(r => r.Type.Should().Be(ReferenceType.Tag));
        references.Should().AllSatisfy(r => r.Element.Should().NotBeNullOrEmpty());
    }

    [Test]
    public void In_TagSimpleTagSample_ShouldReturnExpectedCount()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleTag());

        var references = CrossReference.In(element).ToList();

        references.Should().HaveCount(7);
        references.Should().AllSatisfy(r => r.Reference.Should().NotBeNull());
        references.Should().AllSatisfy(r => r.Scope.Should().Be(Scope.Of(element)));
        references.Should().AllSatisfy(r => r.Type.Should().Be(ReferenceType.Tag));
        references.Should().AllSatisfy(r => r.Element.Should().NotBeNullOrEmpty());
    }

    [Test]
    public void In_TagComplexTagSample_ShouldReturnExpectedCount()
    {
        var element = XElement.Parse(Sample.TagElement.TestComplexTag());

        var references = CrossReference.In(element).ToList();

        references.Should().NotBeEmpty();
        references.Should().AllSatisfy(r => r.Reference.Should().NotBeNull());
        references.Should().AllSatisfy(r => r.Scope.Should().Be(Scope.Of(element)));
        references.Should().AllSatisfy(r => r.Type.Should().Be(ReferenceType.Tag));
        references.Should().AllSatisfy(r => r.Element.Should().NotBeNullOrEmpty());
    }

    [Test]
    public void GettingDistinctReferencesInComplexTag()
    {
        var element = XElement.Parse(Sample.TagElement.TestComplexTag());

        var references = CrossReference.In(element).DistinctBy(r => r.Reference).ToList();

        references.Should().HaveCount(14);
    }

    [Test]
    public void In_MainProgramRung0_ShouldReturnExpected()
    {
        var element = XElement.Parse(Sample.RungElement.MainProgramRung0());

        var references = CrossReference.In(element).ToList();

        references.Should().HaveCount(2);
        references.Should().AllSatisfy(r => r.Reference.Should().NotBeNull());
        references.Should().AllSatisfy(r => r.Scope.Should().Be(Scope.Of(element)));
        references.Should().AllSatisfy(r => r.Type.Should().Be(ReferenceType.Logic));
        references.Should().AllSatisfy(r => r.Element.Should().NotBeNullOrEmpty());
    }
    
    [Test]
    public void In_MainProgramRung2_ShouldReturnExpected()
    {
        var element = XElement.Parse(Sample.RungElement.MainProgramRung2());

        var references = CrossReference.In(element).ToList();

        references.Should().HaveCount(5);
        references.Should().AllSatisfy(r => r.Reference.Should().NotBeNull());
        references.Should().AllSatisfy(r => r.Scope.Should().Be(Scope.Of(element)));
        references.Should().AllSatisfy(r => r.Type.Should().Be(ReferenceType.Logic));
        references.Should().AllSatisfy(r => r.Element.Should().NotBeNullOrEmpty());
    }
}