``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
AMD Ryzen 7 5700G with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|                     Method |        Mean |     Error |   StdDev |
|--------------------------- |------------:|----------:|---------:|
|         FnaVector2Subtract |  4,388.3 ns |  10.04 ns |  7.84 ns |
|         SysVector2Subtract |    509.7 ns |   3.80 ns |  3.37 ns |
| FnaVector2SubtractNoInline | 11,354.9 ns | 104.46 ns | 97.72 ns |
| SysVector2SubtractNoInline |  1,370.3 ns |  12.32 ns | 10.92 ns |
