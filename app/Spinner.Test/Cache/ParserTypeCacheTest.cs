using Spinner.Internals.Cache;
using Spinner.Parsers;
using Spinner.Test.Helper.Parses;
using Xunit;

namespace Spinner.Test.Cache
{
    public class ParserTypeCacheTest
    {
        [Fact]
        public void TryGet_WhenCalled_ShouldReturnTypeParserFromCache()
        {
            // Arrange
            const string key = "key";
            ITypeParser parser = new CacheParser();

            TypeParserCache.Add(key, parser);

            // Act
            bool typeCached = TypeParserCache.TryGet(key, out ITypeParser typeInCache);

            // Assert
            Assert.True(typeCached);
            Assert.IsType<CacheParser>(typeInCache);
        }
    }
}