using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Spinner.Test")]

namespace Spinner.Internals.Cache
{
    internal static class InterceptorCache
    {
        private static readonly ConcurrentDictionary<Type, object> cache = new ConcurrentDictionary<Type, object>();

        public static Interceptors.IInterceptor<T> GetOrAdd<T>(Type interceptorType)
        {
            return (Interceptors.IInterceptor<T>)cache.GetOrAdd(interceptorType, static t => Activator.CreateInstance(t));
        }

        public static bool TryGet<T>(Type key, out Interceptors.IInterceptor<T> output)
        {
            if (cache.TryGetValue(key, out object value))
            {
                output = (Interceptors.IInterceptor<T>)value;
                return true;
            }

            output = default;
            return false;
        }
    }
}