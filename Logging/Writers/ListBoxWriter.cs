using System;
using System.Windows.Forms;

namespace Logging.Writers
{
    /// <summary>
    /// Logs the output to a ListBox control.
    /// </summary>
    public class ListBoxWriter : iLogWriter
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
        void iLogWriter.close()
        {
        }

        /// <summary>
        /// Opens the writer
        /// </summary>
        void iLogWriter.open()
        {
        }

        /// <summary>
        /// Writes a line to the writer.
        /// </summary>
        void iLogWriter.write(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            if (_listbox.InvokeRequired)
            {
                _listbox.BeginInvoke(new Action(()=>Append(pMsg)));
            }
            else
            {
                Append(pMsg);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListBoxWriter(ListBox pListBox)
        {
            _listbox = pListBox;
        }
    }
}