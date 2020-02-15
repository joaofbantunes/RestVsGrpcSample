# RestVsGrpcSample

Not at all serious comparison, just to see both in action and play around with them.

Current version running on .NET Core / ASP.NET Core 3.0.

Don't take the benchmark too seriously, I haven't invested much time in trying to make sure it's accurate. Feel free to open an issue or PR if you see something that could make it more accurate.

``` ini

BenchmarkDotNet=v0.12.0, OS=ubuntu 19.10
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT


```
|    Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----------:|----------:|----------:|------:|--------:|-----:|---------:|------:|------:|----------:|
| RestAsync |  8.963 ms | 0.0912 ms | 0.0762 ms |  1.00 |    0.00 |    1 |  62.5000 |     - |     - | 465.33 KB |
| GrpcAsync | 11.400 ms | 0.1124 ms | 0.1051 ms |  1.27 |    0.02 |    2 | 125.0000 |     - |     - | 818.16 KB |

