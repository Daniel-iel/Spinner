using Spinner.Exceptions;
using System.Reflection;

namespace Spinner.Guards
{
    internal static class Guard
    {
        public static class ReadProperty
        {
            public static void NotMapped(PropertyInfo property, Attribute.ReadPropertyAttribute attribute)
            {
                if (attribute is null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have ReadProperty configured.");
                }
            }
        }

        public static class WriteProperty
        {
            public static void NotMapped(PropertyInfo property, Attribute.WritePropertyAttribute attribute)
            {
                if (attribute is null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have WriteProperty configured.");
                }
            }
        }
    }
}
