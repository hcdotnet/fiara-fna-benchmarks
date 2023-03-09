``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
AMD Ryzen 7 5700G with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|                   Method |        Mean |     Error |    StdDev |
|------------------------- |------------:|----------:|----------:|
|         FnaVector2Divide |  4,379.9 ns |  21.80 ns |  20.39 ns |
|         SysVector2Divide |    776.1 ns |   5.34 ns |   4.73 ns |
| FnaVector2DivideNoInline | 13,336.4 ns | 159.16 ns | 148.88 ns |
| SysVector2DivideNoInline |  1,582.1 ns |   9.68 ns |   8.09 ns |
