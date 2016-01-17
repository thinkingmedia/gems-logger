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
        private bool ThreadFilter(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            return _invert ? !pMsg.Contains(_contains) : pMsg.Contains(_contains);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainsWriter(ILogWriter pWriter, string pContains, bool pInvert)
            : base(pWriter)
        {
            _contains = pContains;
            _invert = pInvert;
            Filter = ThreadFilter;
        }
    }
}