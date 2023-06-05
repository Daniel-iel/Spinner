using Spinner.Cache;
using Spinner.Parsers;
using Spinner.Test.Helper.Parses;
using Xunit;

namespace Spinner.Test.Cache
{
    public class ParserTypeCacheTest
    {
        [Fact]
        public void TryGet_WhenCalled_ShouldReturnParsedTypeFromCache()
        {
            // Arrange
            const string key = "key";
            ITypeParse parser = new CacheParser();

            ParserTypeCache.Add(key, parser);
            // Act
            bool typeCached = ParserTypeCache.TryGet(key, out ITypeParse typeInCache);

            // Assert
            Assert.True(typeCached);
            Assert.IsType<CacheParser>(typeInCache);
        }
    }
}