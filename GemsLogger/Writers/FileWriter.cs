﻿using System;
using System.IO;
using GemsLogger.Exceptions;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Writes each entry out to a text file.
    /// </summary>
    public class FileWriter : ILogWriter, IDisposable
    {
        /// <summary>
        /// The file to Write too.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The output stream writer.
        /// </summary>
        private StreamWriter _writer;

        /// <summary>
        /// Closes the stream.
        /// </summary>
        void IDisposable.Dispose()
        {
            ((ILogWriter)this).Close();
        }

        /// <summary>
        /// Closes the Log file.
        /// </summary>
        void ILogWriter.Close()
        {
            if (_writer != null)
            {
                _writer.Close();
            }
            _writer = null;
        }

        /// <summary>
        /// Opens the file file.
        /// </summary>
        void ILogWriter.Open()
        {
            if (_writer == null)
            {
                _writer = new StreamWriter(_path, true);
            }
        }

        /// <summary>
        /// Writes a line to the Log file.
        /// </summary>
        void ILogWriter.Write(Logger.eLEVEL level, string prefix, string msg)
        {
            if (_writer == null)
            {
                return;
            }
            _writer.WriteLine(msg);
            _writer.Flush();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileWriter(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new LoggerException("Path can not be empty or null.");
            }

            _path = path;

            string dir = Path.GetDirectoryName(_path);
            if (dir == null)
            {
                return;
            }

            Directory.CreateDirectory(dir);
        }
    }
}