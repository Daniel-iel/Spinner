﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Spinner.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a property has no configuration attributes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PropertyNotMappedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the PropertyNotMappedException class with a specified error
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PropertyNotMappedException(string message) : base(message)
        {
        }

        public PropertyNotMappedException() : base(string.Empty)
        {
        }

        public PropertyNotMappedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}