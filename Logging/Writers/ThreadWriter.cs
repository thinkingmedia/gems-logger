using System.Threading;

namespace Logging.Writers
{
    /// <summary>
    /// Only writes log messages that originate from a specific thread ID.
    /// </summary>
    public class ThreadWriter : FilterWriter
    {
        /// <summary>
        /// The thread ID to filter by.
        /// </summary>
        public int ThreadID { get; set; }

        /// <summary>
        /// The filter function.
        /// </summary>
        private bool ThreadFilter(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            return (ThreadID == Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ThreadWriter(iLogWriter pWriter, int pThreadID = 0)
            : base(pWriter)
        {
            ThreadID = pThreadID;
            Filter = ThreadFilter;
        }
    }
}