using System;
using System.Reflection;

namespace Spinner.Test.Helper
{
    internal static class FileInspect<T>
    {
        public static Type Type => typeof(T);

        public static object[] GetAttributes(bool inherit = false)
        {
            return Type.GetCustomAttributes(inherit);
        }

        public static ConstructorInfo[] GetConstructors()
        {
            return Type.GetConstructors();
        }
    }
}
