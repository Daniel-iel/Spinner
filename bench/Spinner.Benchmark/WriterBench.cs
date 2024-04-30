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
        private Nothing instance;
        private NothingWithInterceptor instanceWithParser;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Nothing("Bench Test", "www.bench.com");
            instanceWithParser = new NothingWithInterceptor("Bench Test", "www.bench.com");
        }

        [Benchmark]
        public void WriteAsString()
        {
            Spinner<Nothing> spinner = new Spinner<Nothing>(instance);
            spinner.WriteAsString();
        }

        [Benchmark]
        public void WriteAsSpan()
        {
            Spinner<Nothing> spinner = new Spinner<Nothing>(instance);
            spinner.WriteAsSpan();
        }
    }
}
