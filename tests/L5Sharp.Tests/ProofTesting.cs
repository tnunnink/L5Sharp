using System.Diagnostics;
using FluentAssertions;
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
        var content = L5X.Load(Known.Test);

        var tag = content.FindTag("aoiTestInstance");

        tag.Should().NotBeNull();
    }
}