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
        private void Add(IFormatter formatter)
        {
            _formatters.Add(formatter);
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
        /// <param name="level"></param>
        /// <param name="prefix"></param>
        /// <param name="msg"></param>
        void ILogWriter.Write(Logger.eLEVEL level, string prefix, string msg)
        {
            msg = _formatters.Aggregate(msg, (current, format)=>format.Format(level, prefix, current));
            _writer.Write(level, prefix, msg);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private FormatWriter(ILogWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormatWriter(ILogWriter writer, IFormatter formatter)
            : this(writer)
        {
            Add(formatter);
        }

        /// <summary>
        /// Removes a formatter from the list.
        /// </summary>
        public void Remove(IFormatter formatter)
        {
            _formatters.Remove(formatter);
        }
    }
}