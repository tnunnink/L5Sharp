using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class ScratchTesting
{
    [Test]
    public void Scratch()
    {
        /*var lines = File.ReadAllLines(@"C:\Users\tnunnink\Desktop\types.txt");
        var content = L5X.New("TypeTest", "1756-L83E", "36.01");

        foreach (var line in lines)
        {
            var typeName = line.Trim().Replace(":", "_").Replace("-", "_").Replace(" ", "_");
            var tag = new Tag($"Tag_{typeName}", typeName, $"This is a test tag for type {typeName}");
            content.Tags.Add(tag);
        }

        content.Save(@"C:\users\tnunnink\desktop\predefined.L5X");
        Console.WriteLine($"Generated {lines.Length} tags in file.");*/


        var content = L5X.Load(@"C:\users\tnunnink\documents\rockwell\predefined.L5X");
        content.DataTypes.Clear();

        var dataTypes = content.Tags.Select(t =>
        {
            if (t.Serialize().Elements().All(e => e.Attribute(L5XName.Format)?.Value != DataFormat.Decorated))
                return null;

            var dataType = new DataType(t.DataType);

            var members = t.Value.Members.Select(m => new DataTypeMember
            {
                Name = m.Name,
                DataType = m.Value.Name,
                Dimension = m.Value is ArrayData array ? array.Dimensions : Dimensions.Empty,
                Radix = m.Value is AtomicData atomic ? atomic.Radix : Radix.Null,
                ExternalAccess = ExternalAccess.ReadWrite
            });

            dataType.Members.AddRange(members);
            return dataType;
        });

        content.DataTypes.AddRange(dataTypes);
        content.Save(@"C:\users\tnunnink\documents\rockwell\predefined.L5X");
    }

    [Test]
    public void METHOD()
    {
        var content = L5X.Load(@"C:\users\tnunnink\documents\rockwell\Template.L5X");

        var members = content.DataTypes.SelectMany(d => d.Members).ToList();

        members.Should().NotBeEmpty();
    }
}