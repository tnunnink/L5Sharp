using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Samples;

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