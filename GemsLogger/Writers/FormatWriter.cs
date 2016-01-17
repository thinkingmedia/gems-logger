using System.Collections.Generic;
using System.Linq;
using GemsLogger.Formatters;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Performs formatting of the Log message, and passes it onto another writer.
    /// </summary>
    public class FormatWriter : ILogWriter
    {
        /// <summary>
        /// A list of string formatters that will be used.
        /// </summary>
        private readonly List<IFormatter> _formatters = new List<IFormatter>();

        /// <summary>
        /// The writer object that will perform that actual writing.
        /// </summary>
        private readonly ILogWriter _writer;

        /// <summary>
        /// Adds a formatter to the list.
        /// </summary>
        private void Add(IFormatter pFormatter)
        {
            _formatters.Add(pFormatter);
        }

        /// <summary>
        /// Close the inner writer.
        /// </summary>
        void ILogWriter.Close()
        {
            _writer.Close();
        }

        /// <summary>
        /// Open the inner writer.
        /// </summary>
        void ILogWriter.Open()
        {
            _writer.Open();
        }

        /// <summary>
        /// Writes the message to the Log file, after being formatted.
        /// </summary>
        /// <param name="pLevel"></param>
        /// <param name="pPrefix"></param>
        /// <param name="pMsg"></param>
        void ILogWriter.Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            pMsg = _formatters.Aggregate(pMsg, (pCurrent, pFormat)=>pFormat.format(pLevel, pPrefix, pCurrent));
            _writer.Write(pLevel, pPrefix, pMsg);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private FormatWriter(ILogWriter pWriter)
        {
            _writer = pWriter;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormatWriter(ILogWriter pWriter, IFormatter pFormatter)
            : this(pWriter)
        {
            Add(pFormatter);
        }

        /// <summary>
        /// Removes a formatter from the list.
        /// </summary>
        public void Remove(IFormatter pFormatter)
        {
            _formatters.Remove(pFormatter);
        }
    }
}