using System.Diagnostics;

namespace Logging.Writers
{
    /// <summary>
    /// Sends output to the Visual Studio immediate window.
    /// </summary>
    public class VisualStudioWriter : iLogWriter
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
        public void Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            Debug.WriteLine(pMsg);
        }
    }
}