using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.VSDiagnostics;
using Spinner;
using Spinner.Benchmark.Models;

namespace Writer.Benchmark
{
    [MemoryDiagnoser(true)]
    [CPUUsageDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net90)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [GcServer(true)]
    public class ReaderBenchmark
    {
        Spinner<Person> spinner;
        Spinner<PersonWithInterceptor> spinnerInter;

        [GlobalSetup]
        public void Setup()
        {
            spinner = new Spinner<Person>();
            spinnerInter = new Spinner<PersonWithInterceptor>();
        }

        [Benchmark]
        public void ReadFromString()
        {
            spinner.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromSpan()
        {
            spinner.ReadFromSpan("                                                  ");
        }

        [Benchmark]
        public void ReadFromStringInterceptior()
        {
            spinnerInter.ReadFromString("                                                  ");
        }
    }
}
