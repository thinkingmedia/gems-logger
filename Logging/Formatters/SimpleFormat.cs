using System;

namespace Logging.Formatters
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
            return String.Format("{0}{1}", DetailFormat.Level(pLevel), pMsg);
        }
    }
}