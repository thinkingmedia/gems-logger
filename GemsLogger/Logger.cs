using System;
using System.Collections.Generic;
using System.Diagnostics;
using GemsLogger.Entries;
using GemsLogger.Writers;

namespace GemsLogger
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
        private static readonly List<ILogWriter> _writers = new List<ILogWriter>();

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
        private static void EventLog(string msg)
        {
            string name = AppDomain.CurrentDomain.FriendlyName;
            const string log = "Application";
            if (!System.Diagnostics.EventLog.SourceExists(name))
            {
                System.Diagnostics.EventLog.CreateEventSource(name, log);
            }
            System.Diagnostics.EventLog.WriteEntry(name, msg, EventLogEntryType.Error);

#if DEBUG
            System.Diagnostics.Debug.WriteLine(msg);
#else
                    Console.WriteLine(msg);
#endif
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
        /// <param name="writer">The object that will handle writing.</param>
        public static void Add(ILogWriter writer)
        {
            lock (_writers)
            {
                if (_writers.Contains(writer))
                {
                    return;
                }
                writer.Open();
                _writers.Add(writer);
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
                    ILogWriter writer = _writers[0];
                    try
                    {
                        writer.Close();
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(e.Message);
#else
                    Console.WriteLine(e.Message);
#endif
                    }
                    _writers.Remove(writer);
                }
            }
        }

        /// <summary>
        /// Creates a new logger the uses the given type as a description.
        /// </summary>
        public static Logger Create(Type type)
        {
            return new Logger(type.Name);
        }

        /// <summary>
        /// Logs an Exception to the Windows event Log.
        /// </summary>
        public static void EventLog(Exception ex)
        {
            EventLog(ex.Message);
        }

        /// <summary>
        /// Checks if the logger has an instance of the writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static bool Has(ILogWriter writer)
        {
            lock (_writers)
            {
                return _writers.Contains(writer);
            }
        }

        /// <summary>
        /// Removes a writer from the logger.
        /// </summary>
        public static void Remove(ILogWriter writer)
        {
            lock (_writers)
            {
                if (!_writers.Contains(writer))
                {
                    return;
                }
                writer.Close();
                _writers.Remove(writer);
            }
        }

        /// <summary>
        /// Logs debug information.
        /// </summary>
        public void Debug(string str, params object[] args)
        {
            Log(eLEVEL.DEBUG, string.Format(str, args));
        }

        /// <summary>
        /// Logs an Error.
        /// </summary>
        public void Error(string str, params object[] args)
        {
            Log(eLEVEL.ERROR, string.Format(str, args));
        }

        /// <summary>
        /// Escape a string so it can be sent to the logger without triggering formatting.
        /// </summary>
        public static string Escape(string str)
        {
            return str
                .Replace("{", "{{")
                .Replace("}", "}}");
        }

        /// <summary>
        /// Logs an Exception.
        /// </summary>
        public void Exception(Exception exc)
        {
            // show the stack trace one line at a time
            string[] str = exc
                .ToString()
                .Split(new[] { '\n' });
            string indent = "";
            foreach (string s in str)
            {
                Error(indent + Escape(s.Trim()));
                indent = "    ";
            }
        }

        /// <summary>
        /// Logs information.
        /// </summary>
        public void Fine(string str, params object[] args)
        {
            Log(eLEVEL.FINE, string.Format(str, args));
        }

        /// <summary>
        /// Logs information.
        /// </summary>
        public void Finer(string str, params object[] args)
        {
            Log(eLEVEL.FINER, string.Format(str, args));
        }

        /// <summary>
        /// Logs Finest information.
        /// </summary>
        public void Finest(string str, params object[] args)
        {
            Log(eLEVEL.FINEST, string.Format(str, args));
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        public void Log(eLEVEL level, string msg)
        {
            if (level == eLEVEL.DEBUG && !LogDebug)
            {
                return;
            }

            if (level == eLEVEL.FINEST && !LogFinest)
            {
                return;
            }

            List<ILogWriter> writers;
            lock (_writers)
            {
                writers = new List<ILogWriter>(_writers);
            }

            foreach (ILogWriter writer in writers)
            {
                try
                {
                    writer.Write(level, _prefix, msg);
                }
                catch (Exception e)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(e.Message);
#else
                    Console.WriteLine(e.Message);
#endif
                }
            }
        }

        /// <summary>
        /// Logs a LogEntry.
        /// </summary>
        public void Log(LogEntry entry)
        {
            Log(entry.Level, entry.ToString());
        }
    }
}