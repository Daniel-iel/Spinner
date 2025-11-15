using BenchmarkDotNet.Running;
using System;
using System.Threading;

namespace Writer.Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //var instance = new Nothing("Bench Test", "www.bench.com");
            //var instance2 = new Nothing2("Bench Test", "www.bench.com");

            //// Código original
            //Spinner<Nothing> spinner = new Spinner<Nothing>(instance);

            //// Alteração de struct para class
            //SpinnerV2<Nothing> spinnerV2 = new SpinnerV2<Nothing>(instance);

            //// string builder passado por referência dentro dos métodos
            //SpinnerV3<Nothing> spinnerV3 = new SpinnerV3<Nothing>(instance);

            //// sem passar a instância no construtor, apenas no método de escrita e todas as relexão migrada para o construtor estático, com padding direto no stringbuilder
            //SpinnerV4<Nothing> spinnerV4 = new SpinnerV4<Nothing>();

            ////Sem agressive inlining
            //SpinnerV5<Nothing> spinnerV5 = new SpinnerV5<Nothing>();

            //SpinnerV6<Nothing2> spinnerV6 = new SpinnerV6<Nothing2>();
            //SpinnerV7<Nothing2> spinnerV7 = new SpinnerV7<Nothing2>();

            //const int loop = 5_000_000;

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinner.WriteAsString();
            //}

            //PerfTest.Stop("Write V1: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV2.WriteAsString();
            //}

            //PerfTest.Stop("Write V2: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV3.WriteAsString();
            //}

            //PerfTest.Stop("Write V3: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV4.WriteAsString(instance);
            //}

            //PerfTest.Stop("Write V4: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV5.WriteAsString(instance);
            //}

            //PerfTest.Stop("Write V5: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV6.WriteAsString(instance2);
            //}

            //PerfTest.Stop("Write V6: ", 5_000_000);

            //PerfTest.Start();

            //for (int i = 0; i < 5_000_000; i++)
            //{
            //    spinnerV7.WriteAsString(instance2);
            //}

            //PerfTest.Stop("Write V7: ", 5_000_000);

            BenchmarkRunner.Run<WriterBenchByVersion>();
            //BenchmarkRunner.Run<ReadBench>();
        }
    }
}
public static class PerfTest
{
    static DateTime now;
    static long alloc;

    public static void MeasurePerf(Action action, string name, int repeat)
    {
        PerfTest.Start(action);

        for (int i = 0; i < repeat; i++)
        {
            action();
        }

        PerfTest.Stop(name, repeat, 1);
    }

    public static void Start(Action? warmup = null)
    {
        Console.WriteLine();

        if (warmup != null)
        {
            warmup();
            warmup();
        }

        Thread.Sleep(500);
        GC.Collect(2);
        GC.WaitForPendingFinalizers();
        GC.Collect(2);
        Thread.Sleep(500);

        PerfTest.now = DateTime.UtcNow;

        PerfTest.alloc = GC.GetAllocatedBytesForCurrentThread();
    }

    public static void Stop(string scenario, int count, int repeat = 1)
    {
        double totalTime = (DateTime.UtcNow - PerfTest.now).TotalMilliseconds;
        PerfTest.alloc = GC.GetAllocatedBytesForCurrentThread() - PerfTest.alloc;

        long totalItem = count * repeat;

        if (repeat != 1)
        {
            Console.WriteLine("{0} {1:N2} ms, {2:N0} x {3}", scenario, totalTime, count, repeat);
        }
        else
        {
            Console.WriteLine("{0} {1:N2} ms, {2:N0}", scenario, totalTime, count);
        }

        Console.WriteLine("{0:N3} μs per item", totalTime * 1000 / totalItem);
        Console.WriteLine("Allocation: {0:N0} bytes, {1:N2} per item", alloc, alloc / totalItem);
        Console.WriteLine("Memory usage: {0:N2} mb", GC.GetTotalMemory(false) / 1024 / 1024.0);
    }
}