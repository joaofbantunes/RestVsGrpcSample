# RestVsGrpcSample

Not at all serious comparison, just to see both in action and play around with them.

Current version running on .NET Core / ASP.NET Core 3.0.

Don't take the benchmark too seriously, I haven't invested much time in trying to make sure it's accurate. Feel free to open an issue or PR if you see something that could make it more accurate.

Quick note regarding `ComplexRest` benchmark: with the usage of the stream versions of deserialization, the benchmark results, particularly allocations, oscillate a fair bit, but didn't want to fallback to the string based approach as it's less optimized and I'm trying to keep the comparison as close as possible (but still remember this isn't the most thoroughly thought out benchmark 😛).

``` ini

BenchmarkDotNet=v0.12.0, OS=ubuntu 19.10
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT


```
|      Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|------------ |----------:|----------:|----------:|------:|--------:|-----:|---------:|------:|------:|-----------:|
|        Rest |  9.302 ms | 0.1338 ms | 0.1252 ms |  1.00 |    0.00 |    1 |  62.5000 |     - |     - |  465.67 KB |
|        Grpc | 11.593 ms | 0.1974 ms | 0.1750 ms |  1.25 |    0.02 |    2 | 125.0000 |     - |     - |  818.29 KB |
| ComplexRest | 37.618 ms | 3.3346 ms | 9.8322 ms |  3.19 |    0.87 |    4 | 312.5000 |     - |     - | 2203.48 KB |
| ComplexGrpc | 17.195 ms | 0.3343 ms | 0.3127 ms |  1.85 |    0.04 |    3 | 375.0000 |     - |     - | 2338.66 KB |

// * Warnings *
MultimodalDistribution
  ServiceBenchmark.ComplexRest: Default -> It seems that the distribution is multimodal (mValue = 4.57)