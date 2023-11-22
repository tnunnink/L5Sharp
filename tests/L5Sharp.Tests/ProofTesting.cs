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
    public void Scratch()
    {
        var content = Logix.Load(Known.Test);
        var sheet = content.Query<Sheet>().First();
        var block = sheet[9];

        var arguments = block.Arguments();

        arguments.Should().NotBeEmpty();
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