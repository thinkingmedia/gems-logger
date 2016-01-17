using System;
using System.Windows.Forms;

namespace GemsLogger.Writers
{
    /// <summary>
    /// Logs the output to a ListBox control.
    /// </summary>
    public class ListBoxWriter : ILogWriter
    {
        /// <summary>
        /// The list box control.
        /// </summary>
        private readonly ListBox _listbox;

        /// <summary>
        /// Adds a string to the list control.
        /// </summary>
        private void Append(string pMsg)
        {
            bool scrollBottom = (_listbox.SelectedIndex == -1);

            _listbox.Items.Add(pMsg);

            if (_listbox.Items.Count == 500)
            {
                _listbox.Items.RemoveAt(0);
            }

            if (scrollBottom)
            {
                _listbox.TopIndex = _listbox.Items.Count - 1;
            }

            int width = pMsg.Length * 7;
            if (_listbox.HorizontalExtent < width)
            {
                _listbox.HorizontalExtent = width;
            }
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        void ILogWriter.Close()
        {
        }

        /// <summary>
        /// Opens the writer
        /// </summary>
        void ILogWriter.Open()
        {
        }

        /// <summary>
        /// Writes a line to the writer.
        /// </summary>
        void ILogWriter.Write(Logger.eLEVEL level, string prefix, string msg)
        {
            if (_listbox.InvokeRequired)
            {
                _listbox.BeginInvoke(new Action(()=>Append(msg)));
            }
            else
            {
                Append(msg);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListBoxWriter(ListBox listBox)
        {
            _listbox = listBox;
        }
    }
}