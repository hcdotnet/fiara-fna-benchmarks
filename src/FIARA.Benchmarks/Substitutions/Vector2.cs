extern alias FNA;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using FnaVector2 = FNA::Microsoft.Xna.Framework.Vector2;
using SysVector2 = System.Numerics.Vector2;
using static FIARA.Benchmarks.Substitutions.Vector2Config;

namespace FIARA.Benchmarks.Substitutions;

public static class Vector2Config {
    public const int VECTOR_COUNT = 1000;
}

public class Vector2ConstructorBenchmark {
    private readonly FnaVector2[] fnaVectors = new FnaVector2[VECTOR_COUNT];
    private readonly SysVector2[] sysVectors = new SysVector2[VECTOR_COUNT];

    [Benchmark]
    public void FnaVector2ConstructorInt32MultiCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] = new FnaVector2(i, i);
    }

    [Benchmark]
    public void SysVector2ConstructorInt32MultiCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            sysVectors[i] = new SysVector2(i, i);
    }

    [Benchmark]
    public void FnaVector2ConstructorInt32SingleCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] = new FnaVector2(i);
    }

    [Benchmark]
    public void SysVector2ConstructorInt32SingleCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var f = (float) i;
            sysVectors[i] = new SysVector2(f);
        }
    }

    [Benchmark]
    public void FnaVector2ConstructorSingleMultiCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var f = (float) i;
            fnaVectors[i] = new FnaVector2(f, f);
        }
    }

    [Benchmark]
    public void SysVector2ConstructorSingleMultiCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var f = (float) i;
            sysVectors[i] = new SysVector2(f, f);
        }
    }

    [Benchmark]
    public void FnaVector2ConstructorSingleSingleCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var f = (float) i;
            fnaVectors[i] = new FnaVector2(f);
        }
    }

    [Benchmark]
    public void SysVector2ConstructorSingleSingleCtor() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var f = (float) i;
            sysVectors[i] = new SysVector2(f);
        }
    }
}

public class Vector2AddBenchmark {
    private readonly FnaVector2[] fnaVectors = new FnaVector2[VECTOR_COUNT];
    private readonly SysVector2[] sysVectors = new SysVector2[VECTOR_COUNT];

    [GlobalSetup]
    public void Setup() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            fnaVectors[i] = new FnaVector2(i, i);
            sysVectors[i] = new SysVector2(i, i);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private FnaVector2 FnaAddNoInline(FnaVector2 one, FnaVector2 two) {
        return one + two;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private SysVector2 SysAddNoInline(SysVector2 one, SysVector2 two) {
        return one + two;
    }

    [Benchmark]
    public void FnaVector2Add() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] += fnaVectors[i];
    }

    [Benchmark]
    public void SysVector2Add() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            sysVectors[i] += sysVectors[i];
    }

    [Benchmark]
    public void FnaVector2AddNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = fnaVectors[i];
            fnaVectors[i] = FnaAddNoInline(vec, vec);
        }
    }

    [Benchmark]
    public void SysVector2AddNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = sysVectors[i];
            sysVectors[i] = SysAddNoInline(vec, vec);
        }
    }
}

public class Vector2SubtractBenchmark {
    private readonly FnaVector2[] fnaVectors = new FnaVector2[VECTOR_COUNT];
    private readonly SysVector2[] sysVectors = new SysVector2[VECTOR_COUNT];

    [GlobalSetup]
    public void Setup() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            fnaVectors[i] = new FnaVector2(i, i);
            sysVectors[i] = new SysVector2(i, i);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private FnaVector2 FnaSubtractNoInline(FnaVector2 one, FnaVector2 two) {
        return one - two;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private SysVector2 SysSubtractNoInline(SysVector2 one, SysVector2 two) {
        return one - two;
    }

    [Benchmark]
    public void FnaVector2Subtract() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] -= fnaVectors[i];
    }

    [Benchmark]
    public void SysVector2Subtract() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            sysVectors[i] -= sysVectors[i];
    }

    [Benchmark]
    public void FnaVector2SubtractNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = fnaVectors[i];
            fnaVectors[i] = FnaSubtractNoInline(vec, vec);
        }
    }

    [Benchmark]
    public void SysVector2SubtractNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = sysVectors[i];
            sysVectors[i] = SysSubtractNoInline(vec, vec);
        }
    }
}

public class Vector2MultiplyBenchmark {
    private readonly FnaVector2[] fnaVectors = new FnaVector2[VECTOR_COUNT];
    private readonly SysVector2[] sysVectors = new SysVector2[VECTOR_COUNT];

    [GlobalSetup]
    public void Setup() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            fnaVectors[i] = new FnaVector2(i, i);
            sysVectors[i] = new SysVector2(i, i);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private FnaVector2 FnaMultiplyNoInline(FnaVector2 one, FnaVector2 two) {
        return one * two;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private SysVector2 SysMultiplyNoInline(SysVector2 one, SysVector2 two) {
        return one * two;
    }

    [Benchmark]
    public void FnaVector2Multiply() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] *= fnaVectors[i];
    }

    [Benchmark]
    public void SysVector2Multiply() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            sysVectors[i] *= sysVectors[i];
    }

    [Benchmark]
    public void FnaVector2MultiplyNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = fnaVectors[i];
            fnaVectors[i] = FnaMultiplyNoInline(vec, vec);
        }
    }

    [Benchmark]
    public void SysVector2MultiplyNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = sysVectors[i];
            sysVectors[i] = SysMultiplyNoInline(vec, vec);
        }
    }
}

public class Vector2DivideBenchmark {
    private readonly FnaVector2[] fnaVectors = new FnaVector2[VECTOR_COUNT];
    private readonly SysVector2[] sysVectors = new SysVector2[VECTOR_COUNT];

    [GlobalSetup]
    public void Setup() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            fnaVectors[i] = new FnaVector2(i, i);
            sysVectors[i] = new SysVector2(i, i);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private FnaVector2 FnaDivideNoInline(FnaVector2 one, FnaVector2 two) {
        return one / two;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private SysVector2 SysDivideNoInline(SysVector2 one, SysVector2 two) {
        return one / two;
    }

    [Benchmark]
    public void FnaVector2Divide() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            fnaVectors[i] /= fnaVectors[i];
    }

    [Benchmark]
    public void SysVector2Divide() {
        for (var i = 0; i < VECTOR_COUNT; i++)
            sysVectors[i] /= sysVectors[i];
    }

    [Benchmark]
    public void FnaVector2DivideNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = fnaVectors[i];
            fnaVectors[i] = FnaDivideNoInline(vec, vec);
        }
    }

    [Benchmark]
    public void SysVector2DivideNoInline() {
        for (var i = 0; i < VECTOR_COUNT; i++) {
            var vec = sysVectors[i];
            sysVectors[i] = SysDivideNoInline(vec, vec);
        }
    }
}
