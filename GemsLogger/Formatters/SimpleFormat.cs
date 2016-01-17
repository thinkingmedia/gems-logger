using System;
using System.Threading;

namespace GemsLogger.Formatters
{
    /// <summary>
    /// Writes the Log message with just the level details.
    /// </summary>
    public class SimpleFormat : IFormatter
    {
        /// <summary>
        /// Adds just the Log level to the entry.
        /// </summary>
        string IFormatter.Format(Logger.eLEVEL level, string prefix, string msg)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            return string.Format("[{0}:{1}] {2}{3}", prefix, threadID, DetailFormat.Level(level), msg);
        }
    }
}