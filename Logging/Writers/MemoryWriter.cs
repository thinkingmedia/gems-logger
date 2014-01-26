using System.Text;

namespace Logging.Writers
{
    /// <summary>
    /// Stores the Log entries in memory.
    /// </summary>
    public class MemoryWriter : iLogWriter
    {
        /// <summary>
        /// The buffer.
        /// </summary>
        private StringBuilder _buffer;

        /// <summary>
        /// Close the memory buffer.
        /// </summary>
        void iLogWriter.close()
        {
            _buffer = null;
        }

        /// <summary>
        /// Open the memory buffer.
        /// </summary>
        void iLogWriter.open()
        {
            _buffer = new StringBuilder();
        }

        /// <summary>
        /// Writes to the member buffer.
        /// </summary>
        void iLogWriter.write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            if (_buffer == null)
            {
                return;
            }

            lock (_buffer)
            {
                _buffer.AppendLine(pMsg);
            }
        }

        /// <summary>
        /// Clears the contents of the memory buffer.
        /// </summary>
        public void clear()
        {
            _buffer.Clear();
        }

        /// <summary>
        /// Returns the contents of the writer.
        /// </summary>
        /// <returns></returns>
        public string getBuffer()
        {
            if (_buffer == null)
            {
                return "";
            }

            lock (_buffer)
            {
                return _buffer.ToString().Trim();
            }
        }
    }
}