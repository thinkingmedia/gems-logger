using System;

namespace Logging.Writers
{
    /// <summary>
    /// A single method used to Write to a Log output target.
    /// </summary>
    public interface iLogWriter
    {
        /// <summary>
        /// Closes the writer.
        /// </summary>
        void close();

        /// <summary>
        /// Opens the writer.
        /// </summary>
        void open();

        /// <summary>
        /// Writes a line.
        /// </summary>
        void write(Logger.eLEVEL pLevel, String pPrefix, String pMsg);
    }
}