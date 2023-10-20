using BenchmarkDotNet.Running;
using L5Sharp.Benchmarks;

var summary = BenchmarkRunner.Run<NeutralTextBenchmarks>();