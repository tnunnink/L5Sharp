﻿using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentExportTests
{
    [Test]
    public void Export_ValidComponent_ShouldNotBeNull()
    {
        var controller = new Controller();

        var content = LogixContent.Export(controller);

        content.Should().NotBeNull();
    }

    [Test]
    public Task Export_ValidComponent_ShouldHaveVerifiedL5X()
    {
        var controller = new Controller { Name = "TestController" };

        var content = LogixContent.Export(controller);

        return Verify(content.L5X);
    }

    [Test]
    public Task Export_DataType_ShouldBeValid()
    {
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

        var content = LogixContent.Export(component);

        return Verify(content.L5X);
    }

    [Test]
    public void Export_DataType_WriteToFileForImport()
    {
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

        var content = LogixContent.Export(component);

        content.Should().NotBeNull();

        content.Save(@"C:\Users\tnunnink\Documents\GitHub\L5Sharp\tests\Samples\Generated\DataType.L5X");
    }
}