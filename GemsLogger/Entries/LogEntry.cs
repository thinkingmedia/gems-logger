using System.Linq;
using System.Text;

namespace GemsLogger.Entries
{
    /// <summary>
    /// You can use this class to create dynamic log entries. The data added to the
    /// object will be formatted into a string for the log. Data can be optional or
    /// change the logging level.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Text in the middle.
        /// </summary>
        private readonly StringBuilder _body;

        /// <summary>
        /// Text at the front.
        /// </summary>
        private readonly StringBuilder _prefix;

        /// <summary>
        /// Text at the end.
        /// </summary>
        private readonly StringBuilder _tail;

        /// <summary>
        /// The logging level for this entry.
        /// </summary>
        public Logger.eLEVEL Level { get; set; }

        /// <summary>
        /// Checks parameters for null before writing to builder.
        /// </summary>
        private static void Log(StringBuilder build, bool quoted, string msg, params object[] args)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }
            string str = string.Format(msg, args);
            if (quoted)
            {
                str = string.Format("\"{0}\"", str);
            }
            if (build.Length > 0 && str.Length > 0)
            {
                build.Append(" ");
            }
            build.Append(str);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private LogEntry(Logger.eLEVEL level = Logger.eLEVEL.FINE)
        {
            Level = level;
            _prefix = new StringBuilder();
            _body = new StringBuilder();
            _tail = new StringBuilder();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogEntry(string str, params object[] args)
            : this()
        {
            Add(str, args);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogEntry(Logger.eLEVEL level, string str, params object[] args)
            : this(level)
        {
            Add(str, args);
        }

        /// <summary>
        /// Generates the log message.
        /// </summary>
        public override string ToString()
        {
            string[] parts = {_prefix.ToString().Trim(), _body.ToString().Trim(), _tail.ToString().Trim()};
            return string.Join(" ", from str in parts where str.Length > 0 select str);
        }

        /// <summary>
        /// Adds text to the entry.
        /// </summary>
        public void Add(string str, params object[] args)
        {
            Log(_body, false, str, args);
        }

        /// <summary>
        /// Adds the text to the body of the entry surrounded by quotes.
        /// </summary>
        public void AddQuote(string str, params object[] args)
        {
            Log(_body, true, str, args);
        }

        /// <summary>
        /// Adds text to the start of the entry.
        /// </summary>
        public void Prefix(string str, params object[] args)
        {
            Log(_prefix, false, str, args);
        }

        /// <summary>
        /// Adds text to the start of the entry.
        /// </summary>
        public void PrefixQuote(string str, params object[] args)
        {
            Log(_prefix, true, str, args);
        }

        /// <summary>
        /// Adds text to the end of the entry.
        /// </summary>
        public void Tail(string str, params object[] args)
        {
            Log(_tail, false, str, args);
        }

        /// <summary>
        /// Adds the text to the body of the entry surrounded by quotes.
        /// </summary>
        public void TailQuote(string str, params object[] args)
        {
            Log(_tail, true, str, args);
        }
    }
}