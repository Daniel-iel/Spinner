using Spinner.Cache;
using Spinner.Interceptors;
using Spinner.Test.Helper.Interceptors;
using Xunit;

namespace Spinner.Test.Cache
{
    public class InterceptorCacheTest
    {
        [Fact]
        public void TryGet_WhenCalled_ShouldReturnInterceptorFromCache()
        {
            // Arrange
            const string key = "key";
            IInterceptors interceptor = new CacheInterceptor();

            InterceptorCache.Add(key, interceptor);
            // Act
            var typeCached = InterceptorCache.TryGet(key, out var typeInCache);

            // Assert
            Assert.True(typeCached);
            Assert.IsType<CacheInterceptor>(typeInCache);
        }
    }
}