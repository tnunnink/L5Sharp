using FluentAssertions;
using L5Sharp.Components;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentModuleTests
{
    [Test]
    public void Collection_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var dataTypes = content.Modules().ToList();

        dataTypes.Should().NotBeEmpty();
    }

    [Test]
    public void Add_ValidComponent_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Module
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Modules().Add(component);

        var result = content.Modules().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_ToEmptyFile_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Empty);
        
        var component = new Module
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Modules().Add(component);

        var result = content.Modules().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_AlreadyExists_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Module
        {
            Name = Known.Module,
            Description = "This is a test",
        };

        FluentActions.Invoking(() => content.Modules().Add(component)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Contains_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Contains(Known.Module);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Contains("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Find_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Find(Known.Module);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Module);
    }
    
    [Test]
    public void Find_NonExisting_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Find("Fake");

        result.Should().BeNull();
    }

    [Test]
    public void Get_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Get(Known.Module);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Module);
    }
    
    [Test]
    public void Get_NonExisting_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.Modules().Get("Fake")) .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Remove_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Remove(Known.Module);

        result.Should().BeTrue();

        var component = content.Modules().Find(Known.Module);
        component.Should().BeNull();
    }
    
    [Test]
    public void Remove_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Modules().Remove("Fake");

        result.Should().BeFalse();
    }
    
    [Test]
    public void Replace_Existing_ShouldBeTrueAndExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new Module
        {
            Name = Known.Module,
            Description = "This is a test",
        };

        var result = content.Modules().Replace(replacement);

        result.Should().BeTrue();

        var component = content.Modules().Find(Known.Module);
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.Module);
        component.Description.Should().Be("This is a test");
    }
    
    [Test]
    public void Replace_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new Module
        {
            Name = "Fake",
            Description = "This is a test",
        };

        var result = content.Modules().Replace(replacement);

        result.Should().BeFalse();
    }
    
    [Test]
    public void Upsert_NonExisting_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Module
        {
            Name = "New",
            Description = "This is a test",
        };

        content.Modules().Upsert(component);

        var result = content.Modules().Find("New");
        
        result.Should().NotBeNull();
        result.Name.Should().Be("New");
        result.Description.Should().Be("This is a test");
    }

    [Test]
    public void Upsert_Existing_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Module
        {
            Name = Known.Module,
            Description = "This is a test"
        };

        content.Modules().Upsert(component);

        var result = content.Modules().Find(Known.Module);
        
        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Module);
        result.Description.Should().Be("This is a test");
    }
}