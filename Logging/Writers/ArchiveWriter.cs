using System;

namespace Logging.Writers
{
    /// <summary>
    /// Writes to Log files that incremented as the current date changes.
    /// </summary>
    public class ArchiveWriter : iLogWriter
    {
        /// <summary>
        /// The output folder.
        /// </summary>
        private readonly string _basePath;

        /// <summary>
        /// The Log file _name.
        /// </summary>
        private readonly string _prefix;

        /// <summary>
        /// The full path to the current Log file.
        /// </summary>
        private string _current;

        /// <summary>
        /// The writer for the Log file.
        /// </summary>
        private iLogWriter _writer;

        /// <summary>
        /// Closes the current writer.
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
        /// Opens the writer
        /// </summary>
        void iLogWriter.Open()
        {
            // if the date has changed, then Open the next file.
            string path = String.Format("{0}/{1}-{2}.Log", _basePath, _prefix, DateTime.Today.ToString("yyyy-MM"));
            if (path == _current)
            {
                return;
            }
            ((iLogWriter)this).Close();

            _current = path;
            _writer = new FileWriter(path);
            _writer.Open();
        }

        /// <summary>
        /// Writes a line out to the Log file.
        /// </summary>
        void iLogWriter.Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            ((iLogWriter)this).Open();
            _writer.Write(pLevel, pPrefix, pMsg);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pBasePath">The folder to store Log files.</param>
        /// <param name="pPrefix">Name of the Log file.</param>
        public ArchiveWriter(string pBasePath, string pPrefix)
        {
            _basePath = pBasePath;
            _prefix = pPrefix;
        }
    }
}