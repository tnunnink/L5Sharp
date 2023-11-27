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
    public void GetInstructionMethods()
    {
        
       
        
        var type = typeof(Instruction);

        var method = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.ReturnType == typeof(Instruction) && m.Name == "XIO");
        
        var parameter = Expression.Parameter(typeof(Argument[]), "args");
        
        var function = Expression.Call(method!, parameter);
        
        var factory = Expression.Lambda<Func<Argument[], Instruction>>(function, parameter);

        var instruction = factory.Compile().Invoke(new Argument[]{"MyTagName"});
        
        instruction.Should().NotBeNull();
        instruction.Key.Should().Be("XIO");
        instruction.Signature.Should().Be("XIO(data_bit);");
        instruction.Text.Should().Be("XIO(MyTagName)");
        instruction.Arguments.Should().HaveCount(1);
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