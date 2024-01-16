using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class ProofTesting
{
    [Test]
    public void HowLongDoesThisShitTake()
    {
        var content = L5X.Load(Known.LotOfTags);
        var element = content.Serialize();

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        stopwatch.Stop();
        Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}");

        var tag = new Tag { Name = "MyTimer", Value = new TIMER { PRE = 5000 } };
    }

    [Test]
    public void Scratch()
    {
        var content = Logix.Load(Known.Test);
        var sheet = content.Query<Sheet>().First();

        var references = sheet.References().Where(c => c.Type == nameof(Tag)).ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void Query()
    {
        var content = L5X.Load(Known.Example);

        var test = content.Query<DataTypeMember>().ToList().First();

        var parent = test.Parent;

        parent.Should().NotBeNull();
    }
}
