using System;

namespace GemsLogger.Exceptions
{
    /// <summary>
    /// The base exception for all exceptions in this library.
    /// </summary>
    public class LoggerException : Exception
    {
        /// <summary>
        /// String Format constructor
        /// </summary>
        public LoggerException(string message, params object[] values)
            : base(string.Format(message, values))
        {
        }

        /// <summary>
        /// Inner exception constructor
        /// </summary>
        public LoggerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}