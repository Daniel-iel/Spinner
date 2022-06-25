using System;
using System.Collections.Generic;
using System.Text;

namespace Spinner.Exceptions
{
    public class PropertyNotMappedException : Exception
    {
        public PropertyNotMappedException(string message) : base(message)
        {
        }

    }
}
