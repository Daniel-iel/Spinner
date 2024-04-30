using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Spinner;
using System;

namespace Writer.Benchmark
{
    [MemoryDiagnoser(true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [GcServer(true)]
    public class ReadBench
    {
        private Nothing instance;
        private NothingWithInterceptor instanceWithInterceptor;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Nothing("Bench Test", "www.bench.com");
            instanceWithInterceptor = new NothingWithInterceptor("Bench Test", "www.bench.com");
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

        [Benchmark]
        public void ReadFromStringWithInterceptor()
        {
            Spinner<NothingWithInterceptor> spinner = new Spinner<NothingWithInterceptor>(instanceWithInterceptor);
            spinner.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromString()
        {
            Spinner<Nothing> spinner = new Spinner<Nothing>(instance);
            spinner.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromSpan()
        {
            Spinner<Nothing> spinner = new Spinner<Nothing>(instance);
            spinner.ReadFromSpan("                                                  ".AsSpan());
        }
    }
}
