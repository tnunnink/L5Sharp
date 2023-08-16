using System.Diagnostics;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Samples;
using L5Sharp.Serialization;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Performance.Tests;

[TestFixture]
public class TagTests
{
    [Test]
    public void Deserialize_LargeNumberOfElements_ShouldBePerformant()
    {
        var elements = CreateElements(1000000);
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var tags = elements.Select(LogixSerializer.Deserialize<Tag>).ToList();

        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        tags.Should().HaveCount(elements.Count);
    }

    [Test]
    public void GetTagsToList_FromTestFile_MeasurePerformance()
    {
        var content = LogixContent.Load(Known.Template);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var tags = content.Query<Tag>().SelectMany(t => t.MembersAndSelf()).ToList();

        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        tags.Should().NotBeEmpty();
    }

    [Test]
    public void GetTagsToLookup_FromTestFile_MeasurePerformance()
    {
        var content = LogixContent.Load(Known.Template);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var tags = content.Query<Tag>().SelectMany(t => t.MembersAndSelf()).Select(t => t.TagName.ToString()).ToList();

        File.WriteAllLines(@"C:\Users\tnunnink\Desktop\TagNames10.txt", tags);

        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        Console.WriteLine(tags.Count);
        tags.Should().NotBeEmpty();
    }

    [Test]
    public void GetTagsToLookup_FromGenerated_MeasurePerformance()
    {
        var elements = CreateElements(100000);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var tags = elements.Select(LogixSerializer.Deserialize<Tag>).SelectMany(t => t.MembersAndSelf()).ToList();

        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        Console.WriteLine(tags.Count);
        tags.Should().NotBeEmpty();
    }

    private static List<XElement> CreateElements(int amount)
    {
        var elements = new List<XElement>();

        for (var i = 0; i < amount; i++)
        {
            var element = GenerateTag(i);
            elements.Add(element);
        }

        return elements;
    }

    private static XElement GenerateTag(int index)
    {
        var element = XElement.Parse(
            """
            <Tag Name="Tag" TagType="Base" DataType="DINT" Radix="Decimal" Constant="false" ExternalAccess="None">
                            <Data Format="L5K">
                                <![CDATA[123392]]>
                            </Data>
                            <Data Format="Decorated">
                                <DataValue DataType="DINT" Radix="Decimal" Value="123392"/>
                            </Data>
                        </Tag>
            """);

        element.SetAttributeValue(L5XName.Name, $"Tag_{index}");
        return element;
    }
}