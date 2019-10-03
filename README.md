# RestVsGrpcSample

Not at all serious comparison, just to see both in action and play around with them.

Current version running on .NET Core / ASP.NET Core 3.0.

Don't take the benchmark too seriously, I haven't invested much time in trying to make sure it's accurate. Feel free to open an issue or PR if you see something that could make it more accurate.

``` ini

BenchmarkDotNet=v0.11.3, OS=macOS Mojave 10.14.6 (18G95) [Darwin 18.7.0]
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------- |---------:|----------:|----------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
| RestAsync | 27.72 ms | 0.5527 ms | 0.6143 ms |  1.00 |    0.00 |    1 |    125.0000 |           - |           - |             4.79 KB |
| GrpcAsync | 28.79 ms | 0.2305 ms | 0.1800 ms |  1.03 |    0.02 |    2 |    250.0000 |           - |           - |             7.55 KB |
