using Spinner.Exceptions;

namespace Spinner.Internals.Guards
{
    internal static class Guard
    {
        public static class ReadProperty
        {
            public static void NotMapped<T>(int length)
            {
                if (length == 0)
                {
                    throw new PropertyNotMappedException($"Type {typeof(T).FullName} does not have properties mapped for reading.");
                }
            }
        }

        public static class WriteProperty
        {
            public static void NotMapped<T>(int length)
            {
                if (length == 0)
                {
                    throw new PropertyNotMappedException($"Type {typeof(T).FullName} does not have properties mapped for writing.");
                }
            }
        }

        public static class ObjectMapper
        {
            public static void ValidateLength<T>(ushort length)
            {
                if (length == 0)
                {
                    throw new System.InvalidOperationException($"ObjectMapperAttribute.Length must be greater than 0 for type {typeof(T).FullName}.");
                }
            }
        }
    }
}