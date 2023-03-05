using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentTagTests
{
    [Test]
    public void Collection_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var dataTypes = content.Tags().ToList();

        dataTypes.Should().NotBeEmpty();
    }

    [Test]
    public void Add_ValidComponent_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Tag
        {
            Name = "Test",
            Description = "This is a test",
            Data = new BOOL()
        };

        content.Tags().Add(component);

        var result = content.Tags().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_ToEmptyFile_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Empty);
        
        var component = new Tag
        {
            Name = "Test",
            Description = "This is a test",
            Data = new BOOL()
        };

        content.Tags().Add(component);

        var result = content.Tags().Find("Test");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_AlreadyExists_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Tag
        {
            Name = Known.Tag,
            Description = "This is a test",
            Data = new BOOL()
        };

        FluentActions.Invoking(() => content.Tags().Add(component)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Contains_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Contains(Known.Tag);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Contains("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Find_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Find(Known.Tag);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Tag);
        result.DataType.Should().Be("SimpleType");
        result.Data.Should().NotBeNull();
    }
    
    [Test]
    public void Find_NonExisting_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Find("Fake");

        result.Should().BeNull();
    }

    [Test]
    public void Get_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Get(Known.Tag);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Tag);
        result.DataType.Should().Be("SimpleType");
        result.Data.Should().NotBeNull();
    }
    
    [Test]
    public void Get_NonExisting_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.Tags().Get("Fake")) .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Remove_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Remove(Known.Tag);

        result.Should().BeTrue();

        var component = content.Tags().Find(Known.Tag);
        component.Should().BeNull();
    }
    
    [Test]
    public void Remove_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags().Remove("Fake");

        result.Should().BeFalse();
    }
    
    [Test]
    public void Replace_Existing_ShouldBeTrueAndExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new Tag
        {
            Name = Known.Tag,
            Description = "This is a test",
            Data = new BOOL()
        };

        var result = content.Tags().Replace(replacement);

        result.Should().BeTrue();

        var component = content.Tags().Find(Known.Tag);
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.Tag);
        component.DataType.Should().Be("BOOL");
        component.Data.Should().NotBeNull();
    }
    
    [Test]
    public void Replace_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);
        
        var replacement = new Tag
        {
            Name = "Fake",
            Description = "This is a test",
            Data = new BOOL()
        };

        var result = content.Tags().Replace(replacement);

        result.Should().BeFalse();
    }
    
    [Test]
    public void Upsert_NonExisting_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Tag
        {
            Name = "New",
            Description = "This is a test",
            Data = new TIMER()
        };

        content.Tags().Upsert(component);

        var result = content.Tags().Find("New");
        
        result.Should().NotBeNull();
        result.Name.Should().Be("New");
        result.DataType.Should().Be("TIMER");
        result.Data.Should().NotBeNull();
    }

    [Test]
    public void Upsert_Existing_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new Tag
        {
            Name = Known.Tag,
            Description = "This is a test",
            Data = new BOOL()
        };

        content.Tags().Upsert(component);

        var result = content.Tags().Find(Known.Tag);
        
        result.Should().NotBeNull();
        result.Name.Should().Be(Known.Tag);
        result.Description.Should().Be("This is a test");
        result.Data.Should().NotBeNull();
        result.Data.As<BOOL>().Should().NotBeNull();
    }
}