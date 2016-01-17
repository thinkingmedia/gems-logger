using System;
using System.Diagnostics;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Sends logger output to the console.
    /// </summary>
    public class ConsoleWriter : ILogWriter
    {
        /// <summary>
        /// Send debug to console
        /// </summary>
        private readonly bool _debug;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="debug">True to send debug output to the console.</param>
        public ConsoleWriter(bool debug)
        {
            _debug = debug;
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Opens the writer.
        /// </summary>
        public void Open()
        {
        }

        /// <summary>
        /// Writes a line.
        /// </summary>
        public void Write(Logger.eLEVEL level, string prefix, string msg)
        {
            switch (level)
            {
                case Logger.eLEVEL.ERROR:
                    Console.Error.WriteLine(msg);
                    break;
                case Logger.eLEVEL.DEBUG:
                    if (_debug)
                    {
                        Console.WriteLine(msg);
                    }
                    else
                    {
                        Debug.WriteLine(msg);
                    }
                    break;
                default:
                    Console.WriteLine(msg);
                    break;
            }
        }
    }
}