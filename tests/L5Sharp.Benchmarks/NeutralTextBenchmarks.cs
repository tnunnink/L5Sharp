using BenchmarkDotNet.Attributes;
using L5Sharp.Core;

namespace L5Sharp.Benchmarks;

[MemoryDiagnoser]
public class NeutralTextBenchmarks
{
    private const string Sample =
        "GRT(SimpleInt,400)XIO(MultiDimensionalArray[1,3].3)CMP(ATN(_Test) > 1.0)[TON(TimerArray[0],?,?),OTU(TestComplexTag.SimpleMember.BoolMember)];";
    
    [Params(1000, 10000)]
    public int N;

    private NeutralText _text;

    [GlobalSetup]
    public void Setup()
    {
        _text = new NeutralText(Sample);
    }

    [Benchmark]
    public List<Instruction> Regex()
    {
        var instructions = new List<Instruction>();
        
        for (var i = 0; i < N; i++)
        {
            instructions.AddRange(_text.Instructions());
        }

        return instructions;
    }
}