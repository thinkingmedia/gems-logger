using System;
using System.Collections.Generic;
using System.Diagnostics;
using Logging.Entries;
using Logging.Writers;

namespace Logging
{
    /// <summary>
    /// A simply logging class.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Different levels of logging.
        /// </summary>
        public enum eLEVEL
        {
            FINE,
            FINER,
            FINEST,
            ERROR,
            DEBUG
        };

        /// <summary>
        /// Contains the list of writers that will output content to a Log storage location.
        /// </summary>
        private static readonly List<iLogWriter> _writers = new List<iLogWriter>();

        /// <summary>
        /// Toggles the logging of debug messages.
        /// </summary>
        public static bool LogDebug;

        /// <summary>
        /// Toggles the logging of details to an entry.
        /// </summary>
        public static bool LogDetails = true;

        /// <summary>
        /// Toggles the logging of Finest messages.
        /// </summary>
        public static bool LogFinest;

        /// <summary>
        /// Logging prefix.
        /// </summary>
        private readonly string _prefix;

        /// <summary>
        /// Logs an Error to the Windows event Log.
        /// </summary>
        private static void EventLog(string pMsg)
        {
            string name = AppDomain.CurrentDomain.FriendlyName;
            const string log = "Application";
            if (!System.Diagnostics.EventLog.SourceExists(name))
            {
                System.Diagnostics.EventLog.CreateEventSource(name, log);
            }
            System.Diagnostics.EventLog.WriteEntry(name, pMsg, EventLogEntryType.Error);

            Console.WriteLine(pMsg);
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Logger(string pRefix)
        {
            _prefix = pRefix;
        }

        /// <summary>
        /// Adds a writer to the logger.
        /// </summary>
        /// <param name="pWriter">The object that will handle writing.</param>
        public static void Add(iLogWriter pWriter)
        {
            lock (_writers)
            {
                if (_writers.Contains(pWriter))
                {
                    return;
                }
                pWriter.open();
                _writers.Add(pWriter);
            }
        }

        /// <summary>
        /// Closes all the writers.
        /// </summary>
        public static void Close()
        {
            lock (_writers)
            {
                while (_writers.Count > 0)
                {
                    iLogWriter writer = _writers[0];
                    try
                    {
                        writer.close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    _writers.Remove(writer);
                }
            }
        }

        /// <summary>
        /// Creates a new logger the uses the given type as a description.
        /// </summary>
        public static Logger Create(Type pType)
        {
            return new Logger(pType.Name);
        }

        /// <summary>
        /// Logs an Exception to the Windows event Log.
        /// </summary>
        public static void EventLog(Exception pEx)
        {
            EventLog(pEx.Message);
        }

        /// <summary>
        /// Checks if the logger has an instance of the writer.
        /// </summary>
        /// <param name="pWriter"></param>
        /// <returns></returns>
        public static bool Has(iLogWriter pWriter)
        {
            lock (_writers)
            {
                return _writers.Contains(pWriter);
            }
        }

        /// <summary>
        /// Removes a writer from the logger.
        /// </summary>
        public static void Remove(iLogWriter pWriter)
        {
            lock (_writers)
            {
                if (!_writers.Contains(pWriter))
                {
                    return;
                }
                pWriter.close();
                _writers.Remove(pWriter);
            }
        }

        /// <summary>
        /// Logs debug information.
        /// </summary>
        public void Debug(string pStr, params object[] pArgs)
        {
            Log(eLEVEL.DEBUG, string.Format(pStr, pArgs));
        }

        /// <summary>
        /// Logs an Error.
        /// </summary>
        public void Error(string pStr, params object[] pArgs)
        {
            Log(eLEVEL.ERROR, string.Format(pStr, pArgs));
        }

        /// <summary>
        /// Logs an Exception.
        /// </summary>
        public void Exception(Exception pExc)
        {
            // show the stack trace one line at a time
            string[] str = pExc.ToString().Split(new[] {'\n'});
            string indent = "";
            foreach (string s in str)
            {
                Error(indent + s.Trim());
                indent = "    ";
            }
        }

        /// <summary>
        /// Logs information.
        /// </summary>
        public void Fine(string pStr, params object[] pArgs)
        {
            Log(eLEVEL.FINE, string.Format(pStr, pArgs));
        }

        /// <summary>
        /// Logs information.
        /// </summary>
        public void Finer(string pStr, params object[] pArgs)
        {
            Log(eLEVEL.FINER, string.Format(pStr, pArgs));
        }

        /// <summary>
        /// Logs Finest information.
        /// </summary>
        public void Finest(string pStr, params object[] pArgs)
        {
            Log(eLEVEL.FINEST, string.Format(pStr, pArgs));
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        public void Log(eLEVEL pLevel, string pMsg)
        {
            if (pLevel == eLEVEL.DEBUG && !LogDebug)
            {
                return;
            }

            if (pLevel == eLEVEL.FINEST && !LogFinest)
            {
                return;
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine(pMsg);
#endif

            List<iLogWriter> writers;
            lock (_writers)
            {
                writers = new List<iLogWriter>(_writers);
            }

            foreach (iLogWriter writer in writers)
            {
                try
                {
                    writer.write(pLevel, _prefix, pMsg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Logs a LogEntry.
        /// </summary>
        public void Log(LogEntry pEntry)
        {
            Log(pEntry.Level, pEntry.ToString());
        }
    }
}