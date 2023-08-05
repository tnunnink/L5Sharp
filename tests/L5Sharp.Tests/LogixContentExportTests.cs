using System.Runtime.CompilerServices;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentExportTests
{
    [ModuleInitializer]
    public static void Setup()
    {
        VerifierSettings.AddExtraDatetimeFormat("ddd MMM d HH:mm:ss yyyy");
    }

    [Test]
    public void Export_ValidComponent_ShouldNotBeNull()
    {
        var controller = new Controller { Name = "Test" };

        var content = LogixContent.Export(controller);

        content.Should().NotBeNull();
    }

    [Test]
    public void Export_DataType_WriteToFileForImport()
    {
        const string output = @"C:\Users\tnunnink\Documents\GitHub\L5Sharp\tests\Samples\Generated\DataType.L5X";

        var component = new DataType
        {
            Name = "TestType",
            Description = "this is a test",
            Members = new LogixContainer<DataTypeMember>
            {
                new() { Name = "Timers", DataType = "TIMER", Dimension = new Dimensions(5) },
                new() { Name = "Number", DataType = "DINT", Radix = Radix.Ascii },
                new() { Name = "Flag", DataType = "BOOL", ExternalAccess = ExternalAccess.ReadOnly }
            }
        };

        var content = LogixContent.Export(component);

        content.Should().NotBeNull();

        content.Save(output);

        FileAssert.Exists(output);
        File.Delete(output);
    }
}