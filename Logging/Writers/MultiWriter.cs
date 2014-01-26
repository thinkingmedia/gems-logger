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
        public void open()
        {
            ForEach(pWriter=>pWriter.open());
        }

        /// <summary>
        /// Writes a line.
        /// </summary>
        public void write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            ForEach(pWriter=>pWriter.write(pLevel, pPrefix, pMsg));
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void close()
        {
            ForEach(pWriter=>pWriter.close());
        }
    }
}