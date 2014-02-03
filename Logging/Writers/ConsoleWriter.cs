using System;
using System.Diagnostics;

namespace Logging.Writers
{
    /// <summary>
    /// Sends logger output to the console.
    /// </summary>
    public class ConsoleWriter : iLogWriter
    {
        /// <summary>
        /// Send debug to console
        /// </summary>
        private readonly bool _debug;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pDebug">True to send debug output to the console.</param>
        public ConsoleWriter(bool pDebug)
        {
            _debug = pDebug;
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
        public void Write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            switch (pLevel)
            {
                case Logger.eLEVEL.ERROR:
                    Console.Error.WriteLine(pMsg);
                    break;
                case Logger.eLEVEL.DEBUG:
                    if (_debug)
                    {
                        Console.WriteLine(pMsg);
                    }
                    else
                    {
                        Debug.WriteLine(pMsg);
                    }
                    break;
                default:
                    Console.WriteLine(pMsg);
                    break;
            }
        }
    }
}