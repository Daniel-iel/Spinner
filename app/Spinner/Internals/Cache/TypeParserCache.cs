using Spinner.Parsers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Spinner.Test")]

namespace Spinner.Internals.Cache
{
    internal static class TypeParserCache
    {
        private static readonly Dictionary<string, ITypeParser> _cache = new Dictionary<string, ITypeParser>();

        public static IReadOnlyList<ITypeParser> Parses
        {
            get => _cache.Values.ToImmutableList();
        }

        public static bool Add(string key, ITypeParser value)
        {
            return _cache.TryAdd(key, value);
        }

        public static bool TryGet(string key, out ITypeParser output)
        {
            if (_cache.TryGetValue(key, out ITypeParser value))
            {
                output = value;
                return true;
            }

            output = default;
            return false;
        }
    }
}