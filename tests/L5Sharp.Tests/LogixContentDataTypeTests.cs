using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentDataTypeTests
{
    [Test]
    public void DataTypes_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var dataTypes = content.DataTypes().ToList();

        dataTypes.Should().NotBeEmpty();
    }

    [Test]
    public void Add_ValidComponent_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Test);
        
        var component = new DataType
        {
            Name = "TestType",
            Description = "this is a test",
            Members = new List<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        content.DataTypes().Add(component);

        var result = content.DataTypes().Get("TestType");

        result.Should().NotBeNull();
    }
    
    [Test]
    public void Add_ToEmptyFile_ShouldAddComponent()
    {
        var content = LogixContent.Load(Known.Empty);
        
        var component = new DataType
        {
            Name = "TestType",
            Description = "this is a test",
            Members = new List<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        content.DataTypes().Add(component);

        var result = content.DataTypes().Get("TestType");

        result.Should().NotBeNull();
    }

    [Test]
    public void Contains_ValidDataType_shouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Contains("SimpleType");

        result.Should().BeTrue();
    }
    
    [Test]
    public void Contains_NonExistingDataType_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Contains("FakeType");

        result.Should().BeFalse();
    }

}