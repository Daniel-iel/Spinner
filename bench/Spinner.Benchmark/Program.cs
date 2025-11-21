using BenchmarkDotNet.Running;
using Spinner;
using Spinner.Benchmark.Models;

namespace Writer.Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var spinner = new Spinner<PersonWithInterceptor>();
            var person = spinner.ReadFromString("Daniel de Oliveira spinner.com.br0000000000000000");


            BenchmarkRunner.Run<ReaderBenchmark>();
            BenchmarkRunner.Run<WriterBenchmark>();
        }
    }
}
