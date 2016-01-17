using System.Collections.Generic;

namespace GemsLogger.Writers
{
    /// <summary>
    /// A list of writers that all perform writing from the log stream.
    /// </summary>
    public class MultiWriter : List<ILogWriter>, ILogWriter
    {
        /// <summary>
        /// Opens the writer.
        /// </summary>
        public void Open()
        {
            ForEach(writer=>writer.Open());
        }

        /// <summary>
        /// Writes a line.
        /// </summary>
        public void Write(Logger.eLEVEL level, string prefix, string msg)
        {
            ForEach(pWriter=>pWriter.Write(level, prefix, msg));
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