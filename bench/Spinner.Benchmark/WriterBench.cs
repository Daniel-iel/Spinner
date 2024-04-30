using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Spinner;

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
        private ObjectBench instance;
        private ObjectBenchWithParser instanceWithParser;

        [GlobalSetup]
        public void Setup()
        {
            instance = new ObjectBench("Bench Test", "www.bench.com");
            instanceWithParser = new ObjectBenchWithParser("Bench Test", "www.bench.com");
        }

        [Benchmark]
        public void WriteAsString()
        {
            Spinner<ObjectBench> spinner = new Spinner<ObjectBench>(instance);
            spinner.WriteAsString();
        }

        [Benchmark]
        public void WriteAsSpan()
        {
            Spinner<ObjectBench> spinner = new Spinner<ObjectBench>(instance);
            spinner.WriteAsSpan();
        }
    }
}
