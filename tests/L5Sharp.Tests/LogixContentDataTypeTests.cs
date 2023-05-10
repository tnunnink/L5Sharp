using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentDataTypeTests
{
    [Test]
    public void Collection_WhenCalled_ShouldNotBeEmpty()
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
    public void Add_AlreadyExists_ShouldThrowException()
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

        FluentActions.Invoking(() => content.DataTypes().Add(component)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void AddCollection_ValidComponents_ShouldHaveExpectedCount()
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
    public void Clear_WhenCalled_ShouldResetCount()
    {
        var content = LogixContent.Load(Known.Test);

        content.DataTypes().Clear();

        var count = content.DataTypes().Count();

        count.Should().Be(0);
    }

    [Test]
    public void Contains_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Contains(Known.DataType);

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Contains("FakeType");

        result.Should().BeFalse();
    }

    [Test]
    public void Find_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Find(Known.DataType);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.DataType);
        result.Members.Should().NotBeEmpty();
    }

    [Test]
    public void Find_NonExisting_ShouldBeNull()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Find("Fake");

        result.Should().BeNull();
    }

    [Test]
    public void Get_Existing_ShouldBeExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Get(Known.DataType);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.DataType);
        result.Members.Should().NotBeEmpty();
    }

    [Test]
    public void Get_NonExisting_ShouldThrowException()
    {
        var content = LogixContent.Load(Known.Test);

        FluentActions.Invoking(() => content.DataTypes().Get("Fake")).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Remove_Existing_ShouldBeTrue()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Remove(Known.DataType);

        result.Should().BeTrue();

        var component = content.DataTypes().Find("SimpleTyp");
        component.Should().BeNull();
    }

    [Test]
    public void Remove_NonExisting_ShouldBeFalse()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.DataTypes().Remove("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Update_NonExisting_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var component = new DataType
        {
            Name = "NewComponent",
            Description = "This is a new component",
            Members = new List<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        content.DataTypes().Update(component);

        var result = content.DataTypes().Find("NewComponent");

        result.Should().NotBeNull();
        result.Name.Should().Be("NewComponent");
        result.Description.Should().Be("This is a new component");
        result.Members.Should().HaveCount(3);
    }

    [Test]
    public void Update_Existing_ShouldHaveExpected()
    {
        var content = LogixContent.Load(Known.Test);

        var component = new DataType
        {
            Name = Known.DataType,
            Description = "This is a new component",
            Members = new List<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        content.DataTypes().Update(component);

        var result = content.DataTypes().Find(Known.DataType);

        result.Should().NotBeNull();
        result.Name.Should().Be(Known.DataType);
        result.Description.Should().Be("This is a new component");
        result.Members.Should().HaveCount(3);
    }

    [Test]
    public void Update_ValidNameAndAction_ShouldUpdateTheTargetComponent()
    {
        var content = LogixContent.Load(Known.Test);

        content.DataTypes().Update(Known.DataType, d =>
        {
            d.Description = "This is a new description";
            d.Members.Add(new DataTypeMember { Name = "New_Member", DataType = "REAL", Description = "hello"});
        });

        var result = content.DataTypes().Find(Known.DataType);

        result.Description.Should().Be("This is a new description");
        result.Members.SingleOrDefault(m => m.Name == "New_Member").Should().NotBeNull();
    }
}