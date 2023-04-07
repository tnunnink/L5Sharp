using FluentAssertions;
using L5Sharp.Components;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentAddOnInstructionTests
{
    [Test]
    public void Collection_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var dataTypes = content.Instructions().ToList();

        dataTypes.Should().NotBeEmpty();
    }

    [Test]
    public void Add_ValidComponent_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new AddOnInstruction
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Instructions().Add(component);

        var result = content.Instructions().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_ToEmptyFile_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Empty);
        
        var component = new AddOnInstruction
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Instructions().Add(component);

        var result = content.Instructions().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_AlreadyExists_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new AddOnInstruction
        {
            Name = Known.AddOnInstruction,
            Description = "This is a test",
        };

        FluentActions.Invoking(() => content.Instructions().Add(component)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Contains_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Contains(Known.AddOnInstruction);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Contains("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Find_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Find(Known.AddOnInstruction);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.AddOnInstruction);
    }
    
    [Test]
    public void Find_NonExisting_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Find("Fake");

        result.Should().BeNull();
    }

    [Test]
    public void Get_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Get(Known.AddOnInstruction);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.AddOnInstruction);
    }
    
    [Test]
    public void Get_NonExisting_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.Instructions().Get("Fake")) .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Remove_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Remove(Known.AddOnInstruction);

        result.Should().BeTrue();

        var component = content.Instructions().Find(Known.AddOnInstruction);
        component.Should().BeNull();
    }
    
    [Test]
    public void Remove_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Instructions().Remove("Fake");

        result.Should().BeFalse();
    }
    
    [Test]
    public void Replace_Existing_ShouldBeTrueAndExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new AddOnInstruction
        {
            Name = Known.AddOnInstruction,
            Description = "This is a test",
        };

        var result = content.Instructions().Replace(replacement);

        result.Should().BeTrue();

        var component = content.Instructions().Find(Known.AddOnInstruction);
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.AddOnInstruction);
        component.Description.Should().Be("This is a test");
    }
    
    [Test]
    public void Replace_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new AddOnInstruction
        {
            Name = "Fake",
            Description = "This is a test",
        };

        var result = content.Instructions().Replace(replacement);

        result.Should().BeFalse();
    }
    
    [Test]
    public void Upsert_NonExisting_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new AddOnInstruction
        {
            Name = "New",
            Description = "This is a test",
        };

        content.Instructions().Update(component);

        var result = content.Instructions().Find("New");
        
        result.Should().NotBeNull();
        result.Name.Should().Be("New");
        result.Description.Should().Be("This is a test");
    }

    [Test]
    public void Upsert_Existing_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new AddOnInstruction
        {
            Name = Known.AddOnInstruction,
            Description = "This is a test"
        };

        content.Instructions().Update(component);

        var result = content.Instructions().Find(Known.AddOnInstruction);
        
        result.Should().NotBeNull();
        result.Name.Should().Be(Known.AddOnInstruction);
        result.Description.Should().Be("This is a test");
    }
}