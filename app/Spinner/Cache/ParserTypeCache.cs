using Spinner.Parsers;
using System.Collections.Generic;

namespace Spinner.Cache
{
    internal static class ParserTypeCache
    {
        private static readonly Dictionary<string, ITypeParse> cache = new Dictionary<string, ITypeParse>();

        public static bool Add(string key, ITypeParse value)
        {
            return cache.TryAdd(key, value);
        }

        public static bool TryGet(string key, out ITypeParse output)
        {
            if (cache.TryGetValue(key, out ITypeParse value))
            {
                output = value;
                return true;
            }

            output = default;
            return false;
        }
    }
}
