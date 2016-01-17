using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GemsLogger.Exceptions;

namespace GemsLogger.Formatters
{
    /// <summary>
    /// Adds extra details to the Log information.
    /// </summary>
    public class DetailFormat : IFormatter
    {
        /// <summary>
        /// A mapping between thread IDs and job codes.
        /// </summary>
        private static readonly Dictionary<int, string> _codes = new Dictionary<int, string>();

        /// <summary>
        /// Adds details about a Log entry.
        /// </summary>
        string IFormatter.Format(Logger.eLEVEL level, string prefix, string msg)
        {
            lock (_codes)
            {
                const string format = "{0}@{1}: {2:G} [{3}]:{4}{5}";

                int threadID = Thread.CurrentThread.ManagedThreadId;
                string code = _codes.ContainsKey(threadID) ? _codes[threadID] : "thread";

                return string.Format(format, code, threadID, DateTime.Now, prefix, Level(level), msg);
            }
        }

        /// <summary>
        /// Converts the LogLevel to string (INFO is blank).
        /// </summary>
        public static string Level(Logger.eLEVEL level)
        {
            return level != Logger.eLEVEL.FINE ? string.Format(" [{0}] ", level.ToString().ToUpper()) : " ";
        }

        /// <summary>
        /// Associates a job code with the current thread ID.
        /// </summary>
        public static void Register(string code)
        {
            Register(Thread.CurrentThread.ManagedThreadId, code);
        }

        /// <summary>
        /// Associates a job code with a worker thread ID.
        /// </summary>
        public static void Register(int threadID, string code)
        {
            lock (_codes)
            {
                if (_codes.ContainsValue(code))
                {
                    throw new FormatterException("{0} code already registered.", code);
                }
                _codes.Add(threadID, code);
            }
        }

        /// <summary>
        /// Removes the association between a job code and a worker thread ID.
        /// </summary>
        public static void Unregister(string code)
        {
            lock (_codes)
            {
                if (!_codes.ContainsValue(code))
                {
                    return;
                }

                KeyValuePair<int, string> pair = _codes.First(pPair=>pPair.Value == code);
                _codes.Remove(pair.Key);
            }
        }

        /// <summary>
        /// Removes the association between a job code and a worker thread ID.
        /// </summary>
        public static void Unregister(int threadID)
        {
            lock (_codes)
            {
                if (!_codes.ContainsKey(threadID))
                {
                    return;
                }
                _codes.Remove(threadID);
            }
        }
    }
}