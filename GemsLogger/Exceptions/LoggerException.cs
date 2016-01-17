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
        public LoggerException(string pMessage, params object[] pValues)
            : base(string.Format(pMessage, pValues))
        {
        }

        /// <summary>
        /// Inner exception constructor
        /// </summary>
        public LoggerException(string pMessage, Exception pInner)
            : base(pMessage, pInner)
        {
        }
    }
}