using BenchmarkDotNet.Attributes;
using System;
using Spinner;
using BenchmarkDotNet.Jobs;

namespace Writer.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.NetCoreApp31, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [RPlotExporter]
    [GcServer(true)]
    public class WriterBench
    {
        private ObjectBench instance;

        [GlobalSetup]
        public void Setup()
        {
            instance = new ObjectBench("Bench Test", "www.bench.com");
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
