using System.Linq;
using System.Text;

namespace Logging.Entries
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
        private static void Log(StringBuilder pBuild, bool pQuoted, string pStr, params object[] pArgs)
        {
            if (string.IsNullOrEmpty(pStr))
            {
                return;
            }
            string str = string.Format(pStr, pArgs);
            if (pQuoted)
            {
                str = string.Format("\"{0}\"", str);
            }
            if (pBuild.Length > 0 && str.Length > 0)
            {
                pBuild.Append(" ");
            }
            pBuild.Append(str);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private LogEntry(Logger.eLEVEL pLevel = Logger.eLEVEL.FINE)
        {
            Level = pLevel;
            _prefix = new StringBuilder();
            _body = new StringBuilder();
            _tail = new StringBuilder();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogEntry(string pStr, params object[] pArgs)
            : this()
        {
            Add(pStr, pArgs);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogEntry(Logger.eLEVEL pLevel, string pStr, params object[] pArgs)
            : this(pLevel)
        {
            Add(pStr, pArgs);
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
        public void Add(string pStr, params object[] pArgs)
        {
            Log(_body, false, pStr, pArgs);
        }

        /// <summary>
        /// Adds the text to the body of the entry surrounded by quotes.
        /// </summary>
        public void AddQuote(string pStr, params object[] pArgs)
        {
            Log(_body, true, pStr, pArgs);
        }

        /// <summary>
        /// Adds text to the start of the entry.
        /// </summary>
        public void Prefix(string pStr, params object[] pArgs)
        {
            Log(_prefix, false, pStr, pArgs);
        }

        /// <summary>
        /// Adds text to the start of the entry.
        /// </summary>
        public void PrefixQuote(string pStr, params object[] pArgs)
        {
            Log(_prefix, true, pStr, pArgs);
        }

        /// <summary>
        /// Adds text to the end of the entry.
        /// </summary>
        public void Tail(string pStr, params object[] pArgs)
        {
            Log(_tail, false, pStr, pArgs);
        }

        /// <summary>
        /// Adds the text to the body of the entry surrounded by quotes.
        /// </summary>
        public void TailQuote(string pStr, params object[] pArgs)
        {
            Log(_tail, true, pStr, pArgs);
        }
    }
}