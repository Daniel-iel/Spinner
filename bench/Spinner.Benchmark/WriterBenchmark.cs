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
            var trancode = spinner.WriteAsString(instance);
        }

        [Benchmark]
        public void WriteAsSpan()
        {
            var span = spinner.WriteAsSpan(instance);
        }
    }
}
