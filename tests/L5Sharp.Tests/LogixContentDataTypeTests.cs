using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Enums;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentDataTypeTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes.ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void Index_ValidIndex_ShouldNotBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes[0];

        result.Should().NotBeNull();
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.DataTypes[100]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Add_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);
        var count = content.DataTypes.ToList().Count;
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes.Add(component);

        content.DataTypes.ToList().Count.Should().Be(count + 1);
    }

    [Test]
    public Task Add_ValidComponent_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes.Add(component);

        var xml = content.DataTypes.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void AddAfter_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);
        var count = content.DataTypes.ToList().Count;
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].AddAfter(component);

        content.DataTypes.ToList().Count.Should().Be(count + 1);
    }
    
    [Test]
    public void AddAfter_ValidComponent_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].AddAfter(component);

        var result = content.DataTypes[1];

        component.Serialize().ToString().Should().Be(result.Serialize().ToString());
    }
    
    [Test]
    public void AddBefore_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);
        var count = content.DataTypes.ToList().Count;
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].AddBefore(component);

        content.DataTypes.ToList().Count.Should().Be(count + 1);
    }
    
    [Test]
    public void AddBefore_ValidComponent_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].AddBefore(component);

        var result = content.DataTypes[0];

        component.Serialize().ToString().Should().Be(result.Serialize().ToString());
    }

    [Test]
    public void Count_WhenCalled_ShouldNotBeZero()
    {
        var content = LogixContent.Load(Known.Test);

        var count = content.DataTypes.Count();

        count.Should().BeGreaterThan(0);
    }

    [Test]
    public void RemoveAt_ValidIndex_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);
        var count = content.DataTypes.ToList().Count;
        
        content.DataTypes.RemoveAt(1);

        content.DataTypes.Count().Should().Be(count - 1);
    }
    
    [Test]
    public Task RemoveAt_ValidIndex_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);

        content.DataTypes.RemoveAt(1);

        return Verify(content.DataTypes.Serialize().ToString());
    }
    
    [Test]
    public void Replace_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);
        var count = content.DataTypes.ToList().Count;
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].Replace(component);

        content.DataTypes.ToList().Count.Should().Be(count);
    }
    
    [Test]
    public void Replace_ValidComponent_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);
        var component = new DataType
        {
            Name = "TestType", Description = "This is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Member1", DataType = "DINT", Radix = Radix.Binary },
                new() { Name = "Member2", DataType = "BOOL", Hidden = true, Target = "Other", BitNumber = 0 },
                new() { Name = "Member3", DataType = "SomeType", ExternalAccess = ExternalAccess.ReadOnly },
            }
        };

        content.DataTypes[0].Replace(component);

        var result = content.DataTypes[0];

        component.Serialize().ToString().Should().Be(result.Serialize().ToString());
    }

    [Test]
    public void Remove_ValidComponent_ShouldNotLongExists()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = content.DataTypes[0];
        var count = content.DataTypes.Count();
        var name = component.Name;
        
        component.Remove();

        content.DataTypes[0].Name.Should().NotBe(name);
        content.DataTypes.Count().Should().Be(count - 1);
    }

    [Test]
    public void Update_ValidDelegate_ShouldUpdateAllComponents()
    {
        var content = LogixContent.Load(Known.Test);
        
        content.DataTypes.Update(d => d.Description = "This is an update test");

        foreach (var dataType in content.DataTypes)
        {
            dataType.Description.Should().Be("This is an update test");
        }
    }
    
    [Test]
    public Task Update_ValidDelegate_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);
        
        content.DataTypes.Update(d => d.Description = "This is an update test");

        return Verify(content.DataTypes.Serialize().ToString());
    }

    [Test]
    public Task Update_ValidDelegateAndCondition_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);
        
        content.DataTypes.Update(d => d.Description = "This is a update", d => d.Name.Contains("Type"));

        return Verify(content.DataTypes.Serialize().ToString());
    }
}