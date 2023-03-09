``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
AMD Ryzen 7 5700G with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|                Method |        Mean |     Error |    StdDev |
|---------------------- |------------:|----------:|----------:|
|         FnaVector2Add |  4,438.6 ns |  48.32 ns |  45.20 ns |
|         SysVector2Add |    511.3 ns |   4.75 ns |   4.21 ns |
| FnaVector2AddNoInline | 11,325.1 ns | 120.68 ns | 112.89 ns |
| SysVector2AddNoInline |  1,372.5 ns |  19.15 ns |  17.91 ns |
