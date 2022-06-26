using BenchmarkDotNet.Running;
using System;

namespace Writer.Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<WriterBench>();            
        }
    }
}
