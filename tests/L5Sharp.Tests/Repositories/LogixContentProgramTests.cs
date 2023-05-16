using FluentAssertions;
using L5Sharp.Components;

namespace L5Sharp.Tests.Repositories;

[TestFixture]
public class LogixContentProgramTests
{
    [Test]
    public void Collection_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var dataTypes = content.Programs.ToList();

        dataTypes.Should().NotBeEmpty();
    }

    [Test]
    public void Add_ValidComponent_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Program
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Programs.Add(component);

        var result = content.Programs.Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_ToEmptyFile_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Empty);
        
        var component = new Program
        {
            Name = "Test",
            Description = "This is a test",
        };

        content.Programs.Add(component);

        var result = content.Programs.Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_AlreadyExists_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Program
        {
            Name = Known.Program,
            Description = "This is a test",
        };

        FluentActions.Invoking(() => content.Programs.Add(component)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Contains_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Programs.Contains(Known.Program);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Programs.Contains("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Find_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Programs.Find(Known.Program);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Program);
    }
    
    [Test]
    public void Find_NonExisting_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Programs.Find("Fake");

        result.Should().BeNull();
    }

    [Test]
    public void Remove_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        content.Programs.Remove(Known.Program);

        var component = content.Programs.Find(Known.Program);
        component.Should().BeNull();
    }
    
    [Test]
    public void Remove_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        content.Programs.Remove("Fake");

        var component = content.Programs.Find("Fake");
        component.Should().BeNull();
    }

    [Test]
    public void Update_NonExisting_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Program
        {
            Name = "New",
            Description = "This is a test",
        };

        content.Programs.Update(component);

        var result = content.Programs.Find("New");
        
        result.Should().NotBeNull();
        result.Name.Should().Be("New");
        result.Description.Should().Be("This is a test");
    }

    [Test]
    public void Update_Existing_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Program
        {
            Name = Known.Program,
            Description = "This is a test"
        };

        content.Programs.Update(component);

        var result = content.Programs.Find(Known.Program);
        
        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Program);
        result.Description.Should().Be("This is a test");
    }
}