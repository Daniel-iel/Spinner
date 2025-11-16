using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Spinner;
using Spinner.Benchmark.Models;

namespace Writer.Benchmark
{
    [MemoryDiagnoser(true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [GcServer(true)]
    public class WriterBench
    {
        private Person instance;
        private PersonWithInterceptor instanceWithParser;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Person("Bench Test", "www.bench.com");
            instanceWithParser = new PersonWithInterceptor("Bench Test", "www.bench.com");
        }

        [Benchmark]
        public void WriteAsString()
        {
            Spinner<Person> spinner = new Spinner<Person>(instance);
            spinner.WriteAsString();
        }

        [Benchmark]
        public void WriteAsSpan()
        {
            Spinner<Person> spinner = new Spinner<Person>(instance);
            spinner.WriteAsSpan();
        }
    }
}
