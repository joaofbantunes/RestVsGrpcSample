using System;
using BenchmarkDotNet.Running;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ServiceBenchmark>();
        }
    }
}
