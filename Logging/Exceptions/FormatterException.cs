using System;

namespace Logging.Exceptions
{
    /// <summary>
    /// Exceptions related to the formatters.
    /// </summary>
    public class FormatterException : LoggerException
    {
        /// <summary>
        /// String Format constructor
        /// </summary>
        public FormatterException(string pMessage, params object[] pValues)
            : base(pMessage, pValues)
        {
        }

        /// <summary>
        /// Inner exception constructor
        /// </summary>
        public FormatterException(string pMessage, Exception pInner)
            : base(pMessage, pInner)
        {
        }
    }
}