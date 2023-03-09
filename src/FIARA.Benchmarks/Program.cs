﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FIARA.Benchmarks;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[DisassemblyDiagnoser]
internal static class Program {
    private static readonly List<string> benchmarks = new();

    public static void Main(string[] args) {
        // BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

        void combineFiaraResults(string name) {
            CombineBenchmarkResults("FIARA.Benchmarks." + name);
        }

        void combineSubstitutionsResults(string name) {
            combineFiaraResults("Substitutions." + name);
        }

        combineSubstitutionsResults("Vector2ConstructorBenchmark");
        combineSubstitutionsResults("Vector2AddBenchmark");
        combineSubstitutionsResults("Vector2SubtractBenchmark");
        combineSubstitutionsResults("Vector2MultiplyBenchmark");
        combineSubstitutionsResults("Vector2DivideBenchmark");
        
        WriteActionsFile();
    }

    // https://github.com/benchmark-action/github-action-benchmark/issues/69#issuecomment-1279644127
    private static void CombineBenchmarkResults(string name) {
        Program.benchmarks.Add(name);

        var resultsFile = $"{name}.Combined";
        var searchPattern = $"{name}-report-full-compressed.json";
        var resultsDir = Path.Combine("BenchmarkDotNet.Artifacts", "results");
        var resultsPath = Path.Combine(resultsDir, resultsFile + ".json");

        if (!Directory.Exists(resultsDir))
            throw new DirectoryNotFoundException(resultsDir);

        if (File.Exists(resultsPath))
            File.Delete(resultsPath);

        var reports = Directory.GetFiles(
            resultsDir,
            searchPattern,
            SearchOption.TopDirectoryOnly
        );
        if (!reports.Any())
            throw new FileNotFoundException("No files found", searchPattern);

        var combinedReport = JsonNode.Parse(File.ReadAllText(reports.First()))!;
        var t = combinedReport["Title"]!;
        var benchmarks = combinedReport["Benchmarks"]!.AsArray();

        // Rename title whilst keeping original timestamp.
        combinedReport["Title"] = $"{resultsFile}{t.GetValue<string>()[16..]}";

        foreach (var report in reports.Skip(1)) {
            var node = JsonNode.Parse(File.ReadAllText(report))!;
            var array = node["Benchmarks"]!.AsArray();

            foreach (var benchmark in array) {
                // Parsing twice avoids "The node already has a parent"
                // exception.
                benchmarks.Add(JsonNode.Parse(benchmark!.ToJsonString())!);
            }
        }

        File.WriteAllText(resultsPath, combinedReport.ToString());
    }

    private static void WriteActionsFile() {
        var file = @"
# This file is autogenerate. Please do not make changes to it manually. Instead,
# make changes to the generator in `./src/FIARA.Benchmarks/Program.cs`! 

name: Run, Store, and Publish Benchmarks
on:
    push:
        branches:
            - master

permissions:
    contents: write
    deployments: write

jobs:
    benchmark:
        name: Run FIARA.Benchmarks
        runs-on: ubuntu-latest
        steps:
            - uses: actions/checkout@v2
            - uses: actions/setup-dotnet@v1
              with:
                  dotnet-version: 7.0.x
#            - name: Run Benchmarks
#              run: cd src && dotnet run -c ""Release"" --project ""FIARA.Benchmarks/FIARA.Benchmarks.csproj"" --framework ""net7.0"" --filter ""*"" --exporters ""json""
".Trim();

        string storeResulting(string name) {
            return $@"
            - name: Store Resulting Benchmarks (${name})
              uses: rhysd/github-action-benchmark@v1
              with:
                  name: Benchmark.NET Benchmark
                  tool: ""benchmarkdotnet""
                  output-file-path: src/BenchmarkDotNet.Artifacts/results/${name}-report-full-compressed.json
                  github-token: ${{{{ secrets.GITHUB_TOKEN }}}}
                  auto-push: true
                  alert-threshold: ""200%""
                  comment-on-alert: true
                  fail-on-alert: true
                  alert-comment-cc-users: ""@steviegt6""
".TrimEnd();
        }

        string publishResulting(string name) {
            return $@"
            - name: Publish Resulting Benchmarks (${name})
              uses: rhysd/github-action-benchmark@v1
              with:
                  name: Benchmark.NET Benchmark
                  tool: ""benchmarkdotnet""
                  output-file-path: src/BenchmarkDotNet.Artifacts/results/${name}-report-full-compressed.json
                  github-token: ${{{{ secrets.GITHUB_TOKEN }}}}
                  auto-push: true
                  alert-threshold: ""200%""
                  comment-on-alert: true
                  fail-on-alert: true
                  alert-comment-cc-users: ""@steviegt6""
                  gh-repository: ""github.com/hcdotnet/fiara-fna-benchmarks""
".TrimEnd();
        }

        var sb = new StringBuilder(file);
        sb.AppendLine();

        foreach (var benchmark in benchmarks) {
            sb.AppendLine(storeResulting(benchmark));
            sb.AppendLine(publishResulting(benchmark));
        }
        
        var path = Path.Combine("..", ".github", "workflows", "benchmarks.yml");
        File.WriteAllText(path, sb.ToString());
    }
}
