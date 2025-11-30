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
    public class WriterBenchmark
    {
        private Person instance;

        Spinner<Person> spinner;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Person("Bench Test", "www.bench.com");

            spinner = new Spinner<Person>();
        }

        [Benchmark]
        public void WriteAsString()
        {
            spinner.WriteAsString(instance);
        }

        [Benchmark]
        public void WriteAsSpan()
        {
            spinner.WriteAsSpan(instance);
        }
    }
}
