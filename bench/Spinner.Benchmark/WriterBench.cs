using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.VSDiagnostics;
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


    [MemoryDiagnoser(true)]
    [CPUUsageDiagnoser]
    [SimpleJob(RuntimeMoniker.Net90)]
    [GcServer(true)]
    public class WriterBenchByVersion
    {
        private int loops;

        Spinner<Nothing> spinner;
        SpinnerV2<Nothing> spinnerV2;
        SpinnerV3<Nothing> spinnerV3;
        SpinnerV4<Nothing> spinnerV4;
        SpinnerV5<Nothing> spinnerV5;
        SpinnerV6<Nothing2> spinnerV6;
        SpinnerV7<Nothing2> spinnerV7;

        Nothing instance;
        Nothing2 instance2;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Nothing("Bench Test", "www.bench.com");
            instance2 = new Nothing2("Bench Test", "www.bench.com");

            spinner = new Spinner<Nothing>(instance);
            spinnerV2 = new SpinnerV2<Nothing>(instance);
            spinnerV3 = new SpinnerV3<Nothing>(instance);
            spinnerV4 = new SpinnerV4<Nothing>();
            spinnerV5 = new SpinnerV5<Nothing>();
            spinnerV6 = new SpinnerV6<Nothing2>();
            spinnerV7 = new SpinnerV7<Nothing2>();
        }

        [Benchmark]
        public void V1()
        {
            spinner.WriteAsString();
        }

        [Benchmark]
        public void V2()
        {
            spinnerV2.WriteAsSpan();
        }

        [Benchmark]
        public void V3()
        {
            spinnerV3.WriteAsString();
        }

        [Benchmark]
        public void V4()
        {
            spinnerV4.WriteAsString(instance);
        }

        [Benchmark]
        public void V5()
        {
            spinnerV5.WriteAsString(instance);
        }

        [Benchmark]
        public void V6()
        {
            spinnerV6.WriteAsString(instance2);
        }

        [Benchmark]
        public void V7()
        {
            spinnerV7.WriteAsString(instance2);
        }
    }
}
