using System.Diagnostics;
using System.Xml.Linq;
using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class TagPerformanceTests
{
    [Test]
    public void GetTagsToLookup_FromTestFile_MeasurePerformance()
    {
        var content = L5X.Load(Known.LotOfTags);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var tags = content.Query<Tag>().SelectMany(t => t.Members()).Select(t => t.TagName.ToString()).ToList();
        
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
        var tags = elements.Select(LogixSerializer.Deserialize<Tag>).SelectMany(t => t.Members()).ToList();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        tags.Should().NotBeEmpty();
        
        stopwatch.Reset();
        stopwatch.Start();
        var lookup = tags.ToLookup(t => t.TagName);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        
        Console.WriteLine(lookup.Count);
        lookup.Should().NotBeEmpty();
    }
    
    private static IEnumerable<XElement> CreateElements(int amount)
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
                      @"<Tag Name=""Tag"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""None"">
            <Data Format=""L5K"">
            <![CDATA[123392]]>
            </Data>
            <Data Format=""Decorated"">
            <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""123392""/>
            </Data>
            </Tag>");
        
        element.SetAttributeValue(L5XName.Name, $"Tag_{index}");
        return element;
    }

    [Test]
    public void References_CalledOnAllTags_ShouldWorkFine()
    {
        var content = L5X.Load(Known.LotOfTags);

        var tags = content.Tags.ToList();

        tags.Should().NotBeEmpty();
    }
}