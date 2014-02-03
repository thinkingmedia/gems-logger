using System.Collections.Generic;

namespace Logging.Writers
{
    /// <summary>
    /// A list of writers that all perform writing from the log stream.
    /// </summary>
    public class MultiWriter : List<iLogWriter>, iLogWriter
    {
        /// <summary>
        /// Opens the writer.
        /// </summary>
        public void Open()
        {
            ForEach(pWriter=>pWriter.Open());
        }

        /// <summary>
        /// Writes a line.
        /// </summary>
        public void Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            ForEach(pWriter=>pWriter.Write(pLevel, pPrefix, pMsg));
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void Close()
        {
            ForEach(pWriter=>pWriter.Close());
        }
    }
}