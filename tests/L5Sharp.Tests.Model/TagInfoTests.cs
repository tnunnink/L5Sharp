using System.Text.Json;
using L5Sharp.Core;
using L5Sharp.Model;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Model;

[TestFixture]
public class TagInfoTests
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    [Test]
    public void CreateFromExistingCoreClassShouldHaveExpectedProperties()
    {
        var component = new Tag
        {
            Name = "Test",
            Description = "This is a test",
            ExternalAccess = ExternalAccess.ReadOnly,
            Value = new DINT(123)
        };

        TagInfo info = component;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(info.Name, Is.EqualTo(component.TagName.ToString()));
            Assert.That(info.Description, Is.EqualTo(component.Description));
            Assert.That(info.DataType, Is.EqualTo(component.DataType));
            Assert.That(info.Dimensions, Is.EqualTo(component.Dimensions.ToString()));
            Assert.That(info.Radix, Is.EqualTo(component.Radix.Name));
            Assert.That(info.Access, Is.EqualTo(component.ExternalAccess.Name));
            Assert.That(info.Value, Is.EqualTo(123));
        }
    }

    [Test]
    public Task SerializeInfoToJsonShouldHaveVerifiedResult()
    {
        var info = new TagInfo
        {
            Name = "Test",
            Description = "This is a test",
            DataType = "DINT",
            Dimensions = "1,2,3",
            Radix = "Decimal",
            Access = "ReadOnly",
            Value = null,
            Type = "Base",
            Usage = "Normal",
            Class = "Standard",
            Constant = true,
            Alias = "AliasTag",
            Scope = "Program",
            Container = "MainProgram"
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
              "DataType": "DINT",
              "Dimensions": "1,2,3",
              "Radix": "Decimal",
              "Access": "ReadOnly",
              "Value": null,
              "Type": "Base",
              "Usage": "Normal",
              "Class": "Standard",
              "Constant": true,
              "Alias": "AliasTag",
              "Scope": "Program",
              "Container": "MainProgram",
              "Reference": "Reference"
            }
            """;

        Assert.DoesNotThrow(() =>
        {
            var info = JsonSerializer.Deserialize<TagInfo>(json, Options);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(info, Is.Not.Null);
                Assert.That(info.Name, Is.EqualTo("Test"));
                Assert.That(info.Description, Is.EqualTo("This is a test"));
                Assert.That(info.DataType, Is.EqualTo("DINT"));
                Assert.That(info.Dimensions, Is.EqualTo("1,2,3"));
                Assert.That(info.Radix, Is.EqualTo("Decimal"));
                Assert.That(info.Access, Is.EqualTo("ReadOnly"));
                Assert.That(info.Value, Is.Null);
                Assert.That(info.Type, Is.EqualTo("Base"));
                Assert.That(info.Usage, Is.EqualTo("Normal"));
                Assert.That(info.Class, Is.EqualTo("Standard"));
                Assert.That(info.Constant, Is.True);
                Assert.That(info.Alias, Is.EqualTo("AliasTag"));
                Assert.That(info.Scope, Is.EqualTo("Program"));
                Assert.That(info.Container, Is.EqualTo("MainProgram"));
            }
        });
    }
}