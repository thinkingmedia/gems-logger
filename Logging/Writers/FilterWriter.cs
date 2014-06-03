namespace Logging.Writers
{
    /// <summary>
    /// Handles filtering of log output using a delegate function.
    /// </summary>
    public class FilterWriter : iLogWriter
    {
        /// <summary>
        /// The inner writer being used.
        /// </summary>
        private readonly iLogWriter _writer;

        /// <summary>
        /// The filter function.
        /// </summary>
        protected FilterFunc Filter;

        /// <summary>
        /// Constructor
        /// </summary>
        protected FilterWriter(iLogWriter pWriter)
        {
            _writer = pWriter;
            Filter = null;
        }

        /// <summary>
        /// Opens the writer.
        /// </summary>
        public void Open()
        {
            _writer.Open();
        }

        /// <summary>
        /// Passes the write through a filter before it goes to the inner writer.
        /// </summary>
        public void Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            if (Filter != null && Filter(pLevel, pPrefix, pMsg))
            {
                _writer.Write(pLevel, pPrefix, pMsg);
            }
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void Close()
        {
            _writer.Close();
        }

        /// <summary>
        /// The filter method.
        /// </summary>
        protected delegate bool FilterFunc(Logger.eLEVEL pLevel, string pPrefix, string pMsg);
    }
}