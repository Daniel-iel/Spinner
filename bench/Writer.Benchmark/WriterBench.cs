﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Spinner;
using System;

namespace Writer.Benchmark
{
    [MemoryDiagnoser(true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
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

        [Benchmark]
        public void ReadFromStringWithParser()
        {
            Spinner<ObjectBenchWithParser> spinner = new Spinner<ObjectBenchWithParser>(instanceWithParser);
            spinner.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromString()
        {
            Spinner<ObjectBench> spinner = new Spinner<ObjectBench>(instance);
            spinner.ReadFromString("                                                  ");
        }

        [Benchmark]
        public void ReadFromSpan()
        {
            Spinner<ObjectBench> spinner = new Spinner<ObjectBench>(instance);
            spinner.ReadFromSpan("                                                  ".AsSpan());
        }
    }
}