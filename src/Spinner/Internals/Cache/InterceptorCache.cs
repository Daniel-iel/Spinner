using Spinner.Interceptors;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Spinner.Test")]

namespace Spinner.Internals.Cache
{
    internal static class InterceptorCache
    {
        private static readonly ConcurrentDictionary<Type, IInterceptor> cache = new ConcurrentDictionary<Type, IInterceptor>();

        public static IInterceptor GetOrAdd(Type type)
        {
            return cache.GetOrAdd(type, static t => (IInterceptor)Activator.CreateInstance(t));
        }

        public static bool TryGet(Type key, out IInterceptor output)
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