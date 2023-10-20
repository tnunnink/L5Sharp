using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class L5XBasicTests
{
    [Test]
    public void New_KnownTest_ShouldNotBeNull()
    {
        var element = XElement.Load(Known.Test);
        
        var l5X = new L5X(element);

        l5X.Should().NotBeNull();
    }
    
    [Test]
    public void New_LotsOfTags_ShouldNotBeNull()
    {
        var element = XElement.Load(Known.LotOfTags);
        
        var l5X = new L5X(element);

        l5X.Should().NotBeNull();
    }
    
    [Test]
    public void New_Template_ShouldNotBeNull()
    {
        var element = XElement.Load(Known.Example);
        
        var l5X = new L5X(element);

        l5X.Should().NotBeNull();
    }
    
    [Test]
    public void LoadL5XFileUsingTheL5XLoadMethod()
    {
        var content = L5X.Load(Known.Test);

        content.Should().NotBeNull();
    }
    
    [Test]
    public void Info_ValidContent_ShouldHaveExpectedValues()
    {
        var content = L5X.Load(Known.Test);

        content.Info.Should().NotBeNull();
        content.Info.SchemaRevision.Should().Be("1.0");
        content.Info.SoftwareRevision.Should().Be("32.02");
        content.Info.TargetName.Should().Be("TestController");
        content.Info.TargetType.Should().Be("Controller");
        content.Info.ContainsContext.Should().Be(false);
        content.Info.Owner.Should().Be("tnunnink, EN Engineering");
        content.Info.ExportDate.Should().NotBeNull();
    }

    [Test]
    public void Add_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);
        var count = content.DataTypes.Count();
        var dataType = new DataType {Name = "TestAdd"};
        
        content.Add(dataType);
        
        content.DataTypes.Count().Should().Be(count + 1);
    }
    
    [Test]
    public Task Add_ValidComponent_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Test);
        var dataType = new DataType {Name = "TestAdd"};
        
        content.Add(dataType);
        
        return Verify(content.DataTypes.Serialize().ToString());
    }

    [Test]
    public void Find_ContainsElement_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Find<Tag>().ToList();

        results.Should().NotBeEmpty();
    }
    
    [Test]
    public void Find_NoElement_ShouldBeEmpty()
    {
        var content = L5X.Load(Known.Empty);

        var results = content.Find<Tag>().ToList();

        results.Should().BeEmpty();
    }

    [Test]
    public void Find_ValidComponent_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Test);

        var component = content.Find<DataType>(Known.DataType);

        component.Should().NotBeNull();
    }

    [Test]
    public void FindTag_ValidComponent_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Test);
        
        var tag = content.FindTag(Known.Tag);

        tag.Should().NotBeNull();
    }

    [Test]
    public void FindReferences_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);
        var tag = content.FindTag(Known.Tag);

        var references = content.FindReferences(tag).ToList(); 

        references.Should().NotBeEmpty();
    }
    
    [Test]
    public void Serialize_WhenCalled_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Empty);
        
        var result = content.Serialize();

        result.Should().NotBeNull();
    }
    
    [Test]
    public Task Serialize_WhenCalled_ShouldBeValid()
    {
        var content = L5X.Load(Known.Empty);
        
        var result = content.Serialize().ToString();

        return Verify(result);
    }
}