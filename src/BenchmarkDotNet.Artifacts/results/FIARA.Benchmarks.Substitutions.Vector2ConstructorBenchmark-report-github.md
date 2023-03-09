``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
AMD Ryzen 7 5700G with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|                                Method |     Mean |    Error |   StdDev |
|-------------------------------------- |---------:|---------:|---------:|
|   FnaVector2ConstructorInt32MultiCtor | 533.5 ns |  4.18 ns |  3.49 ns |
|   SysVector2ConstructorInt32MultiCtor | 688.5 ns | 13.72 ns | 15.25 ns |
|  FnaVector2ConstructorInt32SingleCtor | 532.9 ns |  3.96 ns |  3.51 ns |
|  SysVector2ConstructorInt32SingleCtor | 463.4 ns |  4.99 ns |  4.17 ns |
|  FnaVector2ConstructorSingleMultiCtor | 507.9 ns |  0.86 ns |  0.76 ns |
|  SysVector2ConstructorSingleMultiCtor | 563.2 ns |  4.07 ns |  3.61 ns |
| FnaVector2ConstructorSingleSingleCtor | 512.3 ns |  5.87 ns |  5.49 ns |
| SysVector2ConstructorSingleSingleCtor | 457.0 ns |  2.21 ns |  2.07 ns |
