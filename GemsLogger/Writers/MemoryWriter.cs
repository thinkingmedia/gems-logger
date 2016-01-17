using System.Text;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Stores the Log entries in memory.
    /// </summary>
    public class MemoryWriter : ILogWriter
    {
        /// <summary>
        /// The buffer.
        /// </summary>
        private StringBuilder _buffer;

        /// <summary>
        /// Close the memory buffer.
        /// </summary>
        void ILogWriter.Close()
        {
            _buffer = null;
        }

        /// <summary>
        /// Open the memory buffer.
        /// </summary>
        void ILogWriter.Open()
        {
            _buffer = new StringBuilder();
        }

        /// <summary>
        /// Writes to the member buffer.
        /// </summary>
        void ILogWriter.Write(Logger.eLEVEL level, string prefix, string msg)
        {
            if (_buffer == null)
            {
                return;
            }

            lock (_buffer)
            {
                _buffer.AppendLine(msg);
            }
        }

        /// <summary>
        /// Clears the contents of the memory buffer.
        /// </summary>
        public void Clear()
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