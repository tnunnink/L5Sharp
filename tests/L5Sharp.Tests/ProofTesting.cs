using System.Diagnostics;
using FluentAssertions;
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
    [TestCase(0)]
    [TestCase(150)]
    [TestCase(200)]
    [TestCase(250)]
    [TestCase(399)]
    [TestCase(400)]
    public void Scratch(int input)
    {
        var character = (char)(input / 200 + 'A');
        Console.WriteLine(character);
    }

    [Test]
    public void Query()
    {
        var content = L5X.Load(Known.Example);

        var rungs = content.Find<Rung>().ToList();

        rungs.Should().NotBeEmpty();
    }
}