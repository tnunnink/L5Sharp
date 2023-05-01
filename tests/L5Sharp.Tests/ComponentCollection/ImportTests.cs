using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Tests.ComponentCollection;

[TestFixture]
public class ImportTests
{
    [Test]
    public void ImportCollection_ValidComponents_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);

        var number = content.DataTypes().Count();

        var components = new List<DataType>
        {
            new() { Name = "testType1" },
            new() { Name = "testType2" }
        };

        content.DataTypes().Add(components);

        content.DataTypes().Should().HaveCount(number + 2);
    }

    [Test]
    public void AddCollection_HasDuplicateNames_ShouldThrowArgumentException()
    {
        var content = LogixContent.Load(Known.Test);

        var components = new List<DataType>
        {
            new() { Name = "testType1" },
            new() { Name = "testType1" }
        };

        FluentActions.Invoking(() => content.DataTypes().Add(components)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void AddCollection_HasInvalidComponentName_ShouldThrowArgumentException()
    {
        var content = LogixContent.Load(Known.Test);

        var components = new List<DataType>
        {
            new() { Name = "testType1" },
            new() { Name = "!@#$" }
        };

        FluentActions.Invoking(() => content.DataTypes().Add(components)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void AddCollection_AddExistingName_ShouldThrowInvalidOperationException()
    {
        var content = LogixContent.Load(Known.Test);

        var components = new List<DataType>
        {
            new() { Name = "testType1" },
            new() { Name = Known.DataType }
        };

        FluentActions.Invoking(() => content.DataTypes().Add(components)).Should().Throw<InvalidOperationException>();
    }
    
    [Test]
    public void DataType_OverwriteAlreadyExists_ShouldHaveNewValues()
    {
        var content = LogixContent.Load(Known.Test);

        var component = new DataType
        {
            Name = Known.DataType,
            Description = "this is a test",
            Members = new List<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        content.DataTypes().Import(component, true);

        var result = content.DataTypes().Get(Known.DataType);

        result.Name.Should().Be(Known.DataType);
        result.Description.Should().Be("this is a test");
        result.Members.Should().Contain(m => m.Name == "Timers");
        result.Members.Should().Contain(m => m.Name == "Number");
        result.Members.Should().Contain(m => m.Name == "Flag");
    }
}