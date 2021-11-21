using System;
using System.Reflection;

namespace Spinner.Test.Helper
{
    internal class FileInpect<T>
    {
        public static Type Type { get => typeof(T); }

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
