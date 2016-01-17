namespace GemsLogger.Writers
{
    /// <summary>
    /// Handles filtering of log output using a delegate function.
    /// </summary>
    public class FilterWriter : ILogWriter
    {
        /// <summary>
        /// The inner writer being used.
        /// </summary>
        private readonly ILogWriter _writer;

        /// <summary>
        /// The filter function.
        /// </summary>
        protected FilterFunc Filter;

        /// <summary>
        /// Constructor
        /// </summary>
        protected FilterWriter(ILogWriter writer)
        {
            _writer = writer;
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
        public void Write(Logger.eLEVEL level, string prefix, string msg)
        {
            if (Filter != null && Filter(level, prefix, msg))
            {
                _writer.Write(level, prefix, msg);
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
        protected delegate bool FilterFunc(Logger.eLEVEL level, string prefix, string msg);
    }
}