using System.Threading;

namespace GemsLogger.Writers
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
        private bool ThreadFilter(Logger.eLEVEL level, string prefix, string msg)
        {
            return (ThreadID == Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ThreadWriter(ILogWriter writer, int threadID = 0)
            : base(writer)
        {
            ThreadID = threadID;
            Filter = ThreadFilter;
        }
    }
}