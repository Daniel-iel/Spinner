using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Microsoft.VSDiagnostics;
using Spinner;
using Spinner.Benchmark.Models;

namespace Writer.Benchmark
{
    [Config(typeof(BenchmarkConfig))]
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
