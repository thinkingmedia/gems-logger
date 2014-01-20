
using System.Threading;

namespace Logging.Writers
{
    /// <summary>
    /// Only writes a log messages that take place in given
    /// thread ID.
    /// </summary>
    public class ThreadWriter : FilterWriter
    {
        /// <summary>
        /// The thread ID to filter by.
        /// </summary>
        public int ThreadID { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ThreadWriter(iLogWriter pWriter, int pThreadID = 0)
            : base(pWriter)
        {
            ThreadID = pThreadID;
            Filter = ThreadFilter;
        }

        /// <summary>
        /// The filter function.
        /// </summary>
        private bool ThreadFilter(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            return (ThreadID == Thread.CurrentThread.ManagedThreadId);
        }
    }
}
