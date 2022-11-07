using Spinner.Cache;
using Spinner.Parsers;
using Spinner.Test.Helper.Parses;
using Xunit;

namespace Spinner.Test.Cache
{
    public class ParserTypeCacheTest
    {
        [Fact]
        public void TryGet_WhenCalled_ShoudReturnParsedTypeFromCache()
        {
            // Arrange
            const string key = "key";
            ITypeParse parser = new CacheParser();

            ParserTypeCache.Add(key, parser);
            // Act
            var typeCached = ParserTypeCache.TryGet(key, out var typeInCache);

            // Assert
            Assert.True(typeCached);
            Assert.IsType<CacheParser>(typeInCache);
        }
    }
}