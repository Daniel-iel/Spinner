using Spinner.Interceptors;
using Spinner.Internals.Cache;
using Spinner.Test.Helper.Interceptors;
using System;
using Xunit;

namespace Spinner.Test.Cache
{
    public class InterceptorCacheTest
    {
        [Fact]
        public void TryGet_WhenCalled_ShouldReturnInterceptorFromCache()
        {
            // Arrange
            IInterceptor interceptor = new CacheInterceptor();
            Type key = interceptor.GetType();

            InterceptorCache.GetOrAdd(key);

            // Act
            var typeCached = InterceptorCache.TryGet(key, out var typeInCache);

            // Assert
            Assert.True(typeCached);
            Assert.IsType<CacheInterceptor>(typeInCache);
        }

        [Fact]
        public void TryGet_WhenKeyNotInCache_ShouldReturnFalseAndDefaultOutput()
        {
            // Arrange
            Type key = typeof(NonExistentInterceptor);

            // Act
            var typeCached = InterceptorCache.TryGet(key, out var typeInCache);

            // Assert
            Assert.False(typeCached);
            Assert.Null(typeInCache);
        }
    }

    internal class NonExistentInterceptor : IInterceptor
    {
        public object Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}