namespace GemsLogger.Writers
{
    /// <summary>
    /// Injects marker text into the log stream.
    /// </summary>
    public class ContainsWriter : FilterWriter
    {
        /// <summary>
        /// The string that the message has to contain.
        /// </summary>
        private readonly string _contains;

        /// <summary>
        /// True to filter messages that don't contain.
        /// </summary>
        private readonly bool _invert;

        /// <summary>
        /// The filter function.
        /// </summary>
        private bool ThreadFilter(Logger.eLEVEL level, string prefix, string msg)
        {
            return _invert ? !msg.Contains(_contains) : msg.Contains(_contains);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainsWriter(ILogWriter writer, string contains, bool invert)
            : base(writer)
        {
            _contains = contains;
            _invert = invert;
            Filter = ThreadFilter;
        }
    }
}