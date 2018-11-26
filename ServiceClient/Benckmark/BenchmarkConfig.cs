using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace ServiceClient.Benckmark
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.GitHub); // ベンチマーク結果を書く時に出力させとくとベンリ
            // Add(MemoryDiagnoser.Default); // missing in 0.9.*

            Add( new Job() { TargetCount = 10, WarmupCount = 10, LaunchCount = 2 });
        }
    }
}
