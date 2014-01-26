namespace Logging.Writers
{
    public class FilterWriter : iLogWriter
    {
        /// <summary>
        /// The filter function.
        /// </summary>
        protected FilterFunc Filter { get; set; }

        /// <summary>
        /// The inner writer being used.
        /// </summary>
        private iLogWriter _writer { get; set; }

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
        public void open()
        {
            _writer.open();
        }

        /// <summary>
        /// Passes the write through a filter before it goes to the inner writer.
        /// </summary>
        public void write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            if (Filter != null && Filter(pLevel, pPrefix, pMsg))
            {
                _writer.write(pLevel, pPrefix, pMsg);
            }
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void close()
        {
            _writer.close();
        }

        /// <summary>
        /// The filter method.
        /// </summary>
        protected delegate bool FilterFunc(Logger.eLEVEL pLevel, string pPrefix, string pMsg);
    }
}