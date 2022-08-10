using System;

namespace Spinner.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a property has no configuration attributes.
    /// </summary>
    public class PropertyNotMappedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the PropertyNotMappedException class with a specified error
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PropertyNotMappedException(string message) : base(message)
        {
        }
    }
}
