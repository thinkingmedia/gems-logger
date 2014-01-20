
using System;

namespace Logging.Writers
{
    /// <summary>
    /// Writes to a Log files that incremented as the current date changes.
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
        /// Constructor
        /// </summary>
        /// <param name="pBasePath">The folder to store Log files.</param>
        /// <param name="pPrefix">Name of the Log file.</param>
        public ArchiveWriter(string pBasePath, string pPrefix)
        {
            _basePath = pBasePath;
            _prefix = pPrefix;
        }

        /// <summary>
        /// Opens the writer
        /// </summary>
        void iLogWriter.open()
        {
            // if the date has changed, then Open the next file.
            string path = String.Format("{0}/{1}-{2}.Log", _basePath, _prefix, DateTime.Today.ToString("yyyy-MM"));
            if (path == _current)
            {
                return;
            }
            ((iLogWriter)this).close();

            _current = path;
            _writer = new FileWriter(path);
            _writer.open();
        }

        /// <summary>
        /// Writes a line out to the Log file.
        /// </summary>
        void iLogWriter.write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            ((iLogWriter)this).open();
            _writer.write(pLevel, pPrefix, pMsg);
        }

        /// <summary>
        /// Closes the current writer.
        /// </summary>
        void iLogWriter.close()
        {
            if (_writer != null)
            {
                _writer.close();
            }
            _writer = null;
        }
    }
}
