using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Logging;
using Logging.Formatters;
using Logging.Writers;

namespace LoggerUI
{
    /// <summary>
    /// A control the prints the contents of the Log file.
    /// </summary>
    public partial class LoggerControl : UserControl
    {
        /// <summary>
        /// Logging
        /// </summary>
        private static readonly Logger _logger = Logger.Create(typeof(LoggerControl));

        /// <summary>
        /// Used to find marker lines in the log stream.
        /// </summary>
        private const string _MARKER = "--MARKER--";

        /// <summary>
        /// The Log writer.
        /// </summary>
        private readonly ThreadWriter _threadWriter;

        /// <summary>
        /// Handles writing to multiple writers.
        /// </summary>
        private readonly MultiWriter _writer;

        /// <summary>
        /// A counter for placing markers in the Log.
        /// </summary>
        private static int _markCounter;

        /// <summary>
        /// Sets what thread this logger filters.
        /// </summary>
        public int threadID
        {
            get { return _threadWriter.ThreadID; }
            set { _threadWriter.ThreadID = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerControl()
        {
            InitializeComponent();

            FormatWriter format = new FormatWriter(new ListBoxWriter(lbLog), new DetailFormat());

            _threadWriter = new ThreadWriter(format);
            _writer = new MultiWriter
                      {
                          new ContainsWriter(_threadWriter, _MARKER, true), 
                          new ContainsWriter(format, _MARKER, false)
                      };
        }

        /// <summary>
        /// Tells the component the application has started.
        /// </summary>
        public void onStart()
        {
            Logger.Add(_writer);
        }

        /// <summary>
        /// Tells the component the application is shutting down.
        /// </summary>
        public void onStop()
        {
            Logger.Remove(_writer);
        }

        /// <summary>
        /// Changes the status of the Log debug flag.
        /// </summary>
        private void onShowDebug(object pSender, EventArgs pEventArgs)
        {
            Logger.LogDebug = chkShowDebug.Checked;
            _logger.Fine("{0} Log debug mode: {1}", _MARKER, Logger.LogDebug.ToString());
        }

        /// <summary>
        /// Changes the status of the Log Finest flag.
        /// </summary>
        private void onShowFinest(object pSender, EventArgs pEventArgs)
        {
            Logger.LogFinest = checkShowFinest.Checked;
            _logger.Fine("{0} Log Finest mode: {1}", _MARKER, Logger.LogFinest.ToString());
        }

        /// <summary>
        /// Changes the status of the Log details flag.
        /// </summary>
        private void onShowDetails(object pSender, EventArgs pEventArgs)
        {
            Logger.LogDetails = checkShowDetails.Checked;
            _logger.Fine("{0} Log details mode: {1}", _MARKER, Logger.LogDetails.ToString());
        }

        /// <summary>
        /// Clears the contents of the logger window.
        /// </summary>
        private void onBtnClear(object pSender, EventArgs pEventArgs)
        {
            lbLog.Items.Clear();
        }

        /// <summary>
        /// Places a marker in the Log file.
        /// </summary>
        private void onBtnMark(object pSender, EventArgs pEventArgs)
        {
            _logger.Fine("{0} ***********************************************", _MARKER);
            _logger.Fine("{0} - {1} -", _MARKER, _markCounter++);
            _logger.Fine("{0} ***********************************************", _MARKER);
        }

        /// <summary>
        /// Right click to clear selection.
        /// </summary>
        private void onMouseDown(object pSender, MouseEventArgs pEventArgs)
        {
            if (pEventArgs.Button == MouseButtons.Right)
            {
                lbLog.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Open the current item in the default browser if it contains a URL.
        /// </summary>
        private void onDoubleClick(object pSender, EventArgs pEventArgs)
        {
            string str = lbLog.SelectedItem.ToString();

            Match match = Regex.Match(str, @"(https?://.*)");
            if (!match.Success)
            {
                return;
            }

            string url = match.Groups[0].Value.Trim();
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(url);
            }
        }
    }
}