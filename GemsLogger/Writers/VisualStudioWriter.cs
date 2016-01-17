using System.Diagnostics;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Sends output to the Visual Studio immediate window.
    /// </summary>
    public class VisualStudioWriter : ILogWriter
    {
        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Opens the writer.
        /// </summary>
        public void Open()
        {
        }

        /// <summary>
        /// Writes a line.
        /// </summary>
        public void Write(Logger.eLEVEL level, string prefix, string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}