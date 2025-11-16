using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Spinner;
using Spinner.Benchmark.Models;

namespace Writer.Benchmark
{
    [MemoryDiagnoser(true)]
    [SimpleJob(RuntimeMoniker.Net90, baseline: true)]
    [GcServer(true)]
    public class ReaderBench
    {
        private Person instance;
        private PersonWithInterceptor instanceWithInterceptor;
        private PearsonWithSpanInterceptor instanceWithSpanInterceptor;

        Spinner<Person> spinner;
        Spinner<PersonWithInterceptor> spinnerInter;

        SpinnerV2<Person> spinnerV2;
        SpinnerV2<PersonWithInterceptor> spinnerInterV2;
        SpinnerV2<PearsonWithSpanInterceptor> spinnerInterSpanV2;

        [GlobalSetup]
        public void Setup()
        {
            instance = new Person("Bench Test", "www.bench.com");
            instanceWithInterceptor = new PersonWithInterceptor("Bench Test", "www.bench.com");
            instanceWithSpanInterceptor = new PearsonWithSpanInterceptor("Bench Test", "www.bench.com");

            spinner = new Spinner<Person>(instance);
            spinnerInter = new Spinner<PersonWithInterceptor>(instanceWithInterceptor);

            spinnerV2 = new SpinnerV2<Person>();
            spinnerInterV2 = new SpinnerV2<PersonWithInterceptor>();
            spinnerInterSpanV2 = new SpinnerV2<PearsonWithSpanInterceptor>();
        }

        [Benchmark]
        public void ReadFromStringV1()
        {
            spinner.ReadFromString("                                                  ");
        }
        [Benchmark]
        public void ReadFromStringV2()
        {
            spinnerV2.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromStringInterceptiosV1()
        {
            spinnerInter.ReadFromString("                                                  ");
        }
        [Benchmark]
        public void ReadFromStringInterceptiosV2()
        {
            spinnerInterV2.ReadFromString("                                                  ");
        }
        [Benchmark]
        public void ReadFromStringInterceptionSpanV2()
        {
            spinnerInterSpanV2.ReadFromString("                                                  ");
        }
    }
}
