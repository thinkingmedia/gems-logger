using System;
using System.IO;
using Logging.Exceptions;

namespace Logging.Writers
{
    /// <summary>
    /// Writes each entry out to a text file.
    /// </summary>
    public class FileWriter : iLogWriter, IDisposable
    {
        /// <summary>
        /// The file to Write too.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The output stream writer.
        /// </summary>
        private StreamWriter _writer;

        /// <summary>
        /// Closes the stream.
        /// </summary>
        void IDisposable.Dispose()
        {
            ((iLogWriter)this).Close();
        }

        /// <summary>
        /// Closes the Log file.
        /// </summary>
        void iLogWriter.Close()
        {
            if (_writer != null)
            {
                _writer.Close();
            }
            _writer = null;
        }

        /// <summary>
        /// Opens the file file.
        /// </summary>
        void iLogWriter.Open()
        {
            if (_writer == null)
            {
                _writer = new StreamWriter(_path, true);
            }
        }

        /// <summary>
        /// Writes a line to the Log file.
        /// </summary>
        void iLogWriter.Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            if (_writer == null)
            {
                return;
            }
            _writer.WriteLine(pMsg);
            _writer.Flush();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileWriter(String pPath)
        {
            if (string.IsNullOrWhiteSpace(pPath))
            {
                throw new LoggerException("Path can not be empty or null.");
            }

            _path = pPath;

            string dir = Path.GetDirectoryName(_path);
            if (dir == null)
            {
                return;
            }

            Directory.CreateDirectory(dir);
        }
    }
}