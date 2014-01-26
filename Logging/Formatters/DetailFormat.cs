using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Logging.Exceptions;

namespace Logging.Formatters
{
    /// <summary>
    /// Adds extra details to the Log information.
    /// </summary>
    public class DetailFormat : iFormatter
    {
        /// <summary>
        /// A mapping between thread IDs and job codes.
        /// </summary>
        private static readonly Dictionary<int, string> _codes = new Dictionary<int, string>();

        /// <summary>
        /// Adds details about a Log entry.
        /// </summary>
        String iFormatter.format(Logger.eLEVEL pLevel, String pPrefix, String pMsg)
        {
            lock (_codes)
            {
                const string format = "{0}@{1}: {2:G} [{3}]:{4}{5}";

                int threadID = Thread.CurrentThread.ManagedThreadId;
                string code = _codes.ContainsKey(threadID) ? _codes[threadID] : "thread";

                return String.Format(format, code, threadID, DateTime.Now, pPrefix, Level(pLevel), pMsg);
            }
        }

        /// <summary>
        /// Converts the LogLevel to string (INFO is blank).
        /// </summary>
        public static String Level(Logger.eLEVEL pLevel)
        {
            return pLevel != Logger.eLEVEL.FINE ? String.Format(" [{0}] ", pLevel.ToString().ToUpper()) : " ";
        }

        /// <summary>
        /// Associates a job code with the current thread ID.
        /// </summary>
        public static void Register(string pCode)
        {
            Register(Thread.CurrentThread.ManagedThreadId, pCode);
        }

        /// <summary>
        /// Associates a job code with a worker thread ID.
        /// </summary>
        public static void Register(int pThreadID, string pCode)
        {
            lock (_codes)
            {
                if (_codes.ContainsValue(pCode))
                {
                    throw new FormatterException("{0} code already registered.", pCode);
                }
                _codes.Add(pThreadID, pCode);
            }
        }

        /// <summary>
        /// Removes the association between a job code and a worker thread ID.
        /// </summary>
        public static void Unregister(string pCode)
        {
            lock (_codes)
            {
                if (!_codes.ContainsValue(pCode))
                {
                    return;
                }

                KeyValuePair<int, string> pair = _codes.First(pPair=>pPair.Value == pCode);
                _codes.Remove(pair.Key);
            }
        }

        /// <summary>
        /// Removes the association between a job code and a worker thread ID.
        /// </summary>
        public static void Unregister(int pThreadID)
        {
            lock (_codes)
            {
                if (!_codes.ContainsKey(pThreadID))
                {
                    return;
                }
                _codes.Remove(pThreadID);
            }
        }
    }
}