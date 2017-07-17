``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7 CPU 930 2.80GHz (Nehalem), ProcessorCount=8
Frequency=2740572 Hz, Resolution=364.8873 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |    Method |      Mean |     Error |    StdDev |
 |---------- |----------:|----------:|----------:|
 | RestAsync | 143.06 ms | 0.7055 ms | 0.5891 ms |
 | GrpcAsync |  27.66 ms | 0.2155 ms | 0.2016 ms |
