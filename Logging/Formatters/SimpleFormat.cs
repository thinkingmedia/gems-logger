using System;
using System.Threading;

namespace GemsLogger.Formatters
{
    /// <summary>
    /// Writes the Log message with just the level details.
    /// </summary>
    public class SimpleFormat : iFormatter
    {
        /// <summary>
        /// Adds just the Log level to the entry.
        /// </summary>
        string iFormatter.format(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            return String.Format("[{0}:{1}] {2}{3}", pPrefix, threadID, DetailFormat.Level(pLevel), pMsg);
        }
    }
}