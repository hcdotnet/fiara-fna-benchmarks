using System;
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
    public static void Main(string[] args) {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        CombineBenchmarkResults();
    }

    // https://github.com/benchmark-action/github-action-benchmark/issues/69#issuecomment-1279644127
    private static void CombineBenchmarkResults() {
        var resultsFile = $"FIARA.Benchmarks.Combined";
        var searchPattern = $"*-report-full-compressed.json";
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
}
