using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FIARA.Benchmarks;

[DisassemblyDiagnoser]
internal static class Program {
    public static void Main(string[] args) {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
