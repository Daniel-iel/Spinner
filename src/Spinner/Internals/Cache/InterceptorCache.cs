using Spinner.Interceptors;
using System.Collections.Generic;
using System.Linq;

namespace Spinner.Internals.Cache
{
    internal static class InterceptorCache
    {
        private static readonly Dictionary<string, IInterceptor> cache = new Dictionary<string, IInterceptor>();

        public static IEnumerable<IInterceptor> Interceptors
        {
            get => cache.Values.ToList();
        }

        public static bool Add(string key, IInterceptor value)
        {
            return cache.TryAdd(key, value);
        }

        public static bool TryGet(string key, out IInterceptor output)
        {
            if (cache.TryGetValue(key, out IInterceptor value))
            {
                output = value;
                return true;
            }

            output = default;
            return false;
        }
    }
}