using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using System.Runtime.InteropServices;

namespace Writer.Benchmark
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddDiagnoser(MemoryDiagnoser.Default);

            // Adiciona CPUUsageDiagnoser apenas no Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                AddDiagnoser(new Microsoft.VSDiagnostics.CPUUsageDiagnoser());
            }

            AddJob(Job.Default.WithRuntime(BenchmarkDotNet.Environments.CoreRuntime.Core10_0).AsBaseline());
            AddJob(Job.Default.WithRuntime(BenchmarkDotNet.Environments.CoreRuntime.Core90));
            AddJob(Job.Default.WithRuntime(BenchmarkDotNet.Environments.CoreRuntime.Core80));
        }
    }
}
