using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class IREFTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var element = new IREF();

        element.Should().NotBeNull();
    }
    
    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var element = new IREF();

        element.ID.Should().Be(0);
        element.X.Should().Be(0);
        element.Y.Should().Be(0);
        element.Location.Should().Be("A1 (0, 0)");
        element.Sheet.Should().BeNull();
        element.Cell.Should().Be("A1");
        element.Operand.Should().BeNull();
        element.HideDesc.Should().BeFalse();
        element.Scope.Should().Be(Scope.Null);
        element.Container.Should().BeEmpty();
        element.IsAttached.Should().BeFalse();
        element.L5X.Should().BeNull();
        element.L5XType.Should().Be(L5XName.IRef);
    }

    [Test]
    public Task New_Overloaded_ShouldBeVerified()
    {
        var element = new IREF
        {
            ID = 1,
            X = 100,
            Y = 100,
            Operand = "TestTag",
            HideDesc = true
        };
        
        return Verify(element.Serialize().ToString());
    }
    
    [Test]
    public void New_ValidElementNoAttributesIRef_ShouldNotBeNull()
    {
        var element = new XElement(L5XName.IRef);

        var reference = new IREF(element);

        reference.Should().NotBeNull();
    }
    
    [Test]
    public void New_ValidElementNoAttributesORef_ShouldNotBeNull()
    {
        var element = new XElement(L5XName.ORef);

        var reference = new IREF(element);

        reference.Should().NotBeNull();
    }
    
    [Test]
    public void New_ValidElement_ShouldHaveExpectedValues()
    {
        var element = new XElement(L5XName.IRef);
        element.SetAttributeValue(L5XName.ID, 1);
        element.SetAttributeValue(L5XName.X, 100);
        element.SetAttributeValue(L5XName.Y, 200);
        element.SetAttributeValue(L5XName.Operand, "TestTag");
        element.SetAttributeValue(L5XName.HideDesc, true);

        var reference = new IREF(element);
        
        reference.ID.Should().Be(1);
        reference.X.Should().Be(100);
        reference.Y.Should().Be(200);
        reference.Operand.Should().Be("TestTag");
        reference.HideDesc.Should().Be(true);
    }

    [Test]
    public void Deserialize_IRefElement_ShouldNotBeNull()
    {
        var element = new XElement(L5XName.IRef);

        var reference = element.Deserialize();

        reference.Should().NotBeNull();
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeSame()
    {
        var element = new IREF();

        var clone = element.Clone();

        clone.Should().NotBeSameAs(element);
    }

    [Test]
    public void References_WhenCalled_ShouldReturnExpected()
    {
        var element = new IREF
        {
            ID = 1,
            X = 100,
            Y = 100,
            Operand = "TestTag",
            HideDesc = true
        };

        var references = element.References().ToList();

        references.Should().HaveCount(1);
        references[0].Key.Should().Be(new ComponentKey("Tag", "TestTag"));
    }
}