using System;
using System.Collections.Concurrent;
using Xunit;
using System.Threading.Tasks;
using Spinner.Test.Helper.Models;

namespace Spinner.Test
{
    public class SpinnerReadThreadSafetyTest
    {
        [Fact]
        public void ReadFromString_WhenCalledFromMultipleThreads_ShouldBeThreadSafe()
        {
            // Arrange
            const int threadCount = 100;
            const int iterationsPerThread = 1000;
            var spinner = new Spinner<NothingReader>();
            var results = new ConcurrentBag<NothingReader>();
            var exceptions = new ConcurrentBag<Exception>();

            // Act
            Parallel.For(0, threadCount, threadIndex =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        string input = $"thread{threadIndex:D3}item{i:D4}".PadLeft(20) + $"url{i:D4}".PadLeft(30);
                        var result = spinner.ReadFromString(input);
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
            Assert.All(results, result =>
            {
                Assert.NotNull(result.Name);
                Assert.NotNull(result.WebSite);
            });
        }

        [Fact]
        public void ReadFromSpan_WhenCalledFromMultipleThreads_ShouldBeThreadSafe()
        {
            // Arrange
            const int threadCount = 50;
            const int iterationsPerThread = 500;
            var spinner = new Spinner<NothingReader>();
            var results = new ConcurrentBag<NothingReader>();
            var exceptions = new ConcurrentBag<Exception>();

            // Act
            Parallel.For(0, threadCount, threadIndex =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        ReadOnlySpan<char> input = ($"thread{threadIndex:D3}item{i:D4}".PadLeft(20) + $"url{i:D4}".PadLeft(30)).AsSpan();
                        var result = spinner.ReadFromSpan(input);
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
            Assert.All(results, result =>
            {
                Assert.NotNull(result.Name);
                Assert.NotNull(result.WebSite);
            });
        }

        [Fact]
        public void ReadFromString_WithInterceptors_ShouldBeThreadSafeWithCache()
        {
            // Arrange
            const int threadCount = 50;
            const int iterationsPerThread = 200;
            var spinner = new Spinner<NothingDecimalReader>();
            var exceptions = new ConcurrentBag<Exception>();
            var results = new ConcurrentBag<decimal>();

            // Act
            Parallel.For(0, threadCount, threadIndex =>
            {
                try
                {
                    for (int i = 0; i < iterationsPerThread; i++)
                    {
                        string input = $"{i:D4}";
                        var result = spinner.ReadFromString(input);
                        results.Add(result.Value);
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
        }

        [Fact]
        public void ReadFromString_StressTest_ShouldHandleHighConcurrency()
        {
            // Arrange
            const int totalOperations = 25000;
            var spinner = new Spinner<NothingReader>();
            var exceptions = new ConcurrentBag<Exception>();
            var readCount = 0;

            // Act
            Parallel.For(0, totalOperations, i =>
            {
                try
                {
                    string input = $"item{i}".PadLeft(20) + $"url{i}".PadLeft(30);
                    _ = spinner.ReadFromString(input);
                    System.Threading.Interlocked.Increment(ref readCount);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            // Assert
            Assert.Empty(exceptions);
            Assert.Equal(totalOperations, readCount);
        }

        [Fact]
        public void ReadFromString_MultipleInstances_ShouldNotInterfere()
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
                    var spinnerReader = new Spinner<NothingReader>();

                    for (int i = 0; i < operationsPerInstance; i++)
                    {
                        string input = $"instance{instanceId}".PadLeft(20) + $"op{i}".PadLeft(30);
                        var deserialized = spinnerReader.ReadFromString(input);

                        if (deserialized.Name.Contains($"instance{instanceId}"))
                        {
                            System.Threading.Interlocked.Increment(ref successCount);
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
