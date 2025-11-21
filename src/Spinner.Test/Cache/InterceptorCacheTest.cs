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
    }
}