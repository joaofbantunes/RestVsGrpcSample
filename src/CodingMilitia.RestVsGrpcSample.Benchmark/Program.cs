using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ServiceBenchmark>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .With(MemoryDiagnoser.Default));
        }
    }
}