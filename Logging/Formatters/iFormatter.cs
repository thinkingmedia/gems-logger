using System;

namespace GemsLogger.Formatters
{
    /// <summary>
    /// Used to handle formatting of Log lines.
    /// </summary>
    public interface iFormatter
    {
        /// <summary>
        /// Formats a Log entry.
        /// </summary>
        String format(Logger.eLEVEL pLevel, String pPrefix, String pMsg);
    }
}