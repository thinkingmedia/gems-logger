using System;

namespace GemsLogger.Exceptions
{
    /// <summary>
    /// Exceptions related to the formatters.
    /// </summary>
    public class FormatterException : LoggerException
    {
        /// <summary>
        /// String Format constructor
        /// </summary>
        public FormatterException(string message, params object[] values)
            : base(message, values)
        {
        }

        /// <summary>
        /// Inner exception constructor
        /// </summary>
        public FormatterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}