using System.Text.Json;
using L5Sharp.Core;
using L5Sharp.Model;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Model;

[TestFixture]
public class ProgramInfoTests
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    [Test]
    public void CreateFromExistingCoreClassShouldHaveExpectedProperties()
    {
        var component = new Program
        {
            Name = "Test",
            Description = "This is a test",
            Disabled = true,
            MainRoutineName = "Main",
            FaultRoutineName = "Fault"
        };

        ProgramInfo info = component;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(info.Name, Is.EqualTo(component.Name));
            Assert.That(info.Description, Is.EqualTo(component.Description));
            Assert.That(info.Disabled, Is.EqualTo(component.Disabled));
            Assert.That(info.MainRoutine, Is.EqualTo(component.MainRoutineName));
            Assert.That(info.FaultRoutine, Is.EqualTo(component.FaultRoutineName));
        }
    }

    [Test]
    public Task SerializeInfoToJsonShouldHaveVerifiedResult()
    {
        var info = new ProgramInfo
        {
            Name = "Test",
            Description = "This is a test",
            Folder = true,
            MainRoutine = "Main",
            FaultRoutine = "Fault",
            Parent = "ParentFolder",
            Task = "SomeTask",
        };

        var json = JsonSerializer.Serialize(info, Options);

        return VerifyJson(json);
    }

    [Test]
    public void DeserializeInfoFromJsonShouldHaveExpectedResult()
    {
        const string json =
            """
            {
              "Name": "Test",
              "Description": "This is a test",
              "Type": "Normal",
              "Class": "Standard",
              "MainRoutine": "Main",
              "FaultRoutine": "Fault",
              "Disabled": true,
              "Folder": true,
              "TestEdits": false,
              "Parent": "ParentFolder",
              "Task": "SomeTask"
            }
            """;

        Assert.DoesNotThrow(() =>
        {
            var info = JsonSerializer.Deserialize<ProgramInfo>(json, Options);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(info, Is.Not.Null);
                Assert.That(info.Name, Is.EqualTo("Test"));
                Assert.That(info.Description, Is.EqualTo("This is a test"));
                Assert.That(info.Type, Is.EqualTo("Normal"));
                Assert.That(info.Class, Is.EqualTo("Standard"));
                Assert.That(info.MainRoutine, Is.EqualTo("Main"));
                Assert.That(info.FaultRoutine, Is.EqualTo("Fault"));
                Assert.That(info.Disabled, Is.True);
                Assert.That(info.Folder, Is.True);
                Assert.That(info.Parent, Is.EqualTo("ParentFolder"));
                Assert.That(info.Task, Is.EqualTo("SomeTask"));
            }
        });
    }
}