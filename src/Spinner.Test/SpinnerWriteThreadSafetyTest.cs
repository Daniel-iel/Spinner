using Spinner.Test.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerWriteThreadSafetyTest
    {
        [Fact]
        public void WriteAsString_WhenCalledFromMultipleThreads_ShouldBeThreadSafe()
        {
            // Arrange
            const int threadCount = 100;
            const int iterationsPerThread = 1000;
            var spinner = new Spinner<NothingPadLeft>();
            var results = new ConcurrentBag<string>();
            var exceptions = new ConcurrentBag<Exception>();

            // Act
            Parallel.For(0, threadCount, threadIndex =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        var obj = new NothingPadLeft($"thread{threadIndex:D3}item{i:D4}", $"url{i:D4}");
                        string result = spinner.WriteAsString(obj);
                        results.Add(result);
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(threadCount * iterationsPerThread, results.Count);

            // Validate all results have correct length
            Assert.All(results, result => Assert.Equal(50, result.Length));
        }

        [Fact]
        public void WriteAsSpan_WhenCalledFromMultipleThreads_ShouldBeThreadSafe()
        {
            // Arrange
            const int threadCount = 50;
            const int iterationsPerThread = 500;
            var spinner = new Spinner<NothingPadLeft>();
            var results = new ConcurrentBag<string>();
            var exceptions = new ConcurrentBag<Exception>();

            // Act
            Parallel.For(0, threadCount, threadIndex =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        var obj = new NothingPadLeft($"thread{threadIndex:D3}", $"item{i:D4}");
                        ReadOnlySpan<char> result = spinner.WriteAsSpan(obj);
                        results.Add(result.ToString());
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(threadCount * iterationsPerThread, results.Count);
            Assert.All(results, result => Assert.Equal(50, result.Length));
        }

        [Fact]
        public void WriteAsString_WithThreadStaticStringBuilder_ShouldNotShareStateAcrossThreads()
        {
            // Arrange
            const int threadCount = 10;
            var spinner = new Spinner<NothingPadLeft>();
            var results = new ConcurrentDictionary<int, List<string>>();
            var barrier = new Barrier(threadCount);

            // Act
            var tasks = Enumerable.Range(0, threadCount).Select(threadId =>
            {
                return Task.Run(() =>
                {
                    var localResults = new List<string>();

                    // Synchronize all threads to start at the same time
                    barrier.SignalAndWait();

                    for (int i = 0; i < 100; i++)
                    {
                        var obj = new NothingPadLeft($"T{threadId}_{i}", $"url_{i}");
                        string result = spinner.WriteAsString(obj);
                        localResults.Add(result);

                        // Small delay to increase chance of detecting race conditions
                        Thread.Sleep(1);
                    }

                    results[threadId] = localResults;
                });
            }).ToArray();

            Task.WaitAll(tasks);

            // Assert
            Assert.Equal(threadCount, results.Count);

            // Verify each thread got unique results based on their input
            foreach (var kvp in results)
            {
                Assert.Equal(100, kvp.Value.Count);
                Assert.All(kvp.Value, result => Assert.Equal(50, result.Length));
            }
        }

        [Fact]
        public void WriteAsSpan_ConcurrentAccess_ShouldNotCorruptMemory()
        {
            // Arrange
            const int threadCount = 20;
            const int iterationsPerThread = 100;
            var spinner = new Spinner<NothingPadLeft>();
            var allResults = new ConcurrentBag<char[]>();
            var exceptions = new ConcurrentBag<Exception>();

            // Act
            Parallel.For(0, threadCount, threadId =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        var obj = new NothingPadLeft($"T{threadId:D3}I{i:D3}", $"U{i:D3}");
                        ReadOnlySpan<char> span = spinner.WriteAsSpan(obj);

                        // Copy span to array to verify later
                        char[] copy = span.ToArray();
                        allResults.Add(copy);
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(threadCount * iterationsPerThread, allResults.Count);

            // Verify no memory corruption - each result should be valid
            Assert.All(allResults, result =>
            {
                Assert.Equal(50, result.Length);
                Assert.DoesNotContain('\0', result.Take(50)); // No null chars in expected range
            });
        }

        [Fact]
        public void WriteAsString_StressTest_ShouldHandleHighConcurrency()
        {
            // Arrange
            const int totalOperations = 25000;
            var spinner = new Spinner<NothingPadLeft>();
            var exceptions = new ConcurrentBag<Exception>();
            var writeCount = 0;

            // Act
            Parallel.For(0, totalOperations, i =>
            {
                try
                {
                    var obj = new NothingPadLeft($"item{i}", $"url{i}");
                    _ = spinner.WriteAsString(obj);
                    Interlocked.Increment(ref writeCount);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(totalOperations, writeCount);
        }

        [Fact]
        public void WriteAsString_MultipleInstances_ShouldNotInterfere()
        {
            // Arrange
            const int instanceCount = 20;
            const int operationsPerInstance = 250;
            var exceptions = new ConcurrentBag<Exception>();
            var successCount = 0;

            // Act
            Parallel.For(0, instanceCount, instanceId =>
            {
                try
                {
                    var spinner = new Spinner<NothingPadLeft>();

                    for (int i = 0; i < operationsPerInstance; i++)
                    {
                        var obj = new NothingPadLeft($"instance{instanceId}", $"op{i}");
                        string serialized = spinner.WriteAsString(obj);

                        if (serialized.Contains($"instance{instanceId}"))
                        {
                            Interlocked.Increment(ref successCount);
                        }
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(instanceCount * operationsPerInstance, successCount);
        }
    }
}
