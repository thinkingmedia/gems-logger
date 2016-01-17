using System;

namespace GemsLogger.Writers
{
    /// <summary>
    /// A single method used to Write to a Log output target.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Closes the writer.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens the writer.
        /// </summary>
        void Open();

        /// <summary>
        /// Writes a line.
        /// </summary>
        void Write(Logger.eLEVEL pLevel, String pPrefix, String pMsg);
    }
}