``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
AMD Ryzen 7 5700G with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|                     Method |        Mean |    Error |   StdDev |
|--------------------------- |------------:|---------:|---------:|
|         FnaVector2Multiply |  4,401.9 ns | 24.59 ns | 20.53 ns |
|         SysVector2Multiply |    514.6 ns |  4.38 ns |  4.10 ns |
| FnaVector2MultiplyNoInline | 11,443.9 ns | 72.28 ns | 67.61 ns |
| SysVector2MultiplyNoInline |  1,377.4 ns | 23.18 ns | 22.77 ns |
