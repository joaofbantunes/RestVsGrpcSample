# RestVsGrpcSample

Not at all serious comparison, just to see both in action and play around with them.

Current version running on .NET Core / ASP.NET Core 3.1.

Don't take the benchmark too seriously, I haven't invested much time in trying to make sure it's accurate. Feel free to open an issue or PR if you see something that could make it more accurate.

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=3.1.200-preview-015002
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|      Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|------------ |---------:|---------:|---------:|------:|--------:|-----:|---------:|------:|------:|-----------:|
|        Rest | 20.38 ms | 0.262 ms | 0.245 ms |  1.00 |    0.00 |    1 |  62.5000 |     - |     - |  477.59 KB |
|        Grpc | 22.46 ms | 0.445 ms | 1.156 ms |  1.04 |    0.11 |    2 | 125.0000 |     - |     - |   855.9 KB |
| ComplexRest | 29.61 ms | 0.569 ms | 0.633 ms |  1.45 |    0.04 |    3 | 312.5000 |     - |     - |    2085 KB |
| ComplexGrpc | 23.42 ms | 0.869 ms | 2.562 ms |  1.33 |    0.06 |    2 | 375.0000 |     - |     - | 2376.08 KB |
