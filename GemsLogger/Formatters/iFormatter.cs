﻿using System;

namespace GemsLogger.Formatters
{
    /// <summary>
    /// Used to handle formatting of Log lines.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats a Log entry.
        /// </summary>
        string Format(Logger.eLEVEL level, string prefix, string msg);
    }
}