using System.Collections.Generic;
using System.Linq;
using Logging.Formatters;

namespace Logging.Writers
{
    /// <summary>
    /// Performs formatting of the Log message, and passes it onto another writer.
    /// </summary>
    public class FormatWriter : iLogWriter
    {
        /// <summary>
        /// A list of string formatters that will be used.
        /// </summary>
        private readonly List<iFormatter> _formatters = new List<iFormatter>();

        /// <summary>
        /// The writer object that will perform that actual writing.
        /// </summary>
        private readonly iLogWriter _writer;

        /// <summary>
        /// Constructor
        /// </summary>
        private FormatWriter(iLogWriter pWriter)
        {
            _writer = pWriter;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormatWriter(iLogWriter pWriter, iFormatter pFormatter)
            : this(pWriter)
        {
            Add(pFormatter);
        }

        /// <summary>
        /// Adds a formatter to the list.
        /// </summary>
        private void Add(iFormatter pFormatter)
        {
            _formatters.Add(pFormatter);
        }

        /// <summary>
        /// Removes a formatter from the list.
        /// </summary>
        public void Remove(iFormatter pFormatter)
        {
            _formatters.Remove(pFormatter);
        }

        /// <summary>
        /// Writes the message to the Log file, after being formatted.
        /// </summary>
        /// <param name="pLevel"></param>
        /// <param name="pPrefix"></param>
        /// <param name="pMsg"></param>
        void iLogWriter.write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            pMsg = _formatters.Aggregate(pMsg, (pCurrent, pFormat) => pFormat.format(pLevel, pPrefix, pCurrent));
            _writer.write(pLevel, pPrefix, pMsg);
        }

        /// <summary>
        /// Open the inner writer.
        /// </summary>
        void iLogWriter.open()
        {
            _writer.open();
        }

        /// <summary>
        /// Close the inner writer.
        /// </summary>
        void iLogWriter.close()
        {
            _writer.close();
        }
    }
}
