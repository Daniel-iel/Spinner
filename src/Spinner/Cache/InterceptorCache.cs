using Spinner.Interceptors;
using System.Collections.Generic;
using System.Linq;

namespace Spinner.Cache
{
    internal static class InterceptorCache
    {
        private static readonly Dictionary<string, IInterceptors> cache = new Dictionary<string, IInterceptors>();

        public static IEnumerable<IInterceptors> Interceptors
        {
            get => cache.Values.ToList();
        }

        public static bool Add(string key, IInterceptors value)
        {
            return cache.TryAdd(key, value);
        }

        public static bool TryGet(string key, out IInterceptors output)
        {
            if (cache.TryGetValue(key, out IInterceptors value))
            {
                output = value;
                return true;
            }

            output = default;
            return false;
        }
    }
}