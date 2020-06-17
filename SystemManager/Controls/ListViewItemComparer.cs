using System;
using System.Collections;
using System.Windows.Forms;

namespace SM.Controls
{
    /// <summary>
    /// Implements the manual sorting of items by columns.
    /// </summary>
    class ListViewItemComparer : IComparer
    {
        private readonly int _column;
        public ListViewItemComparer()
        {
        }
        public ListViewItemComparer(int column)
        {
            this._column = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[_column].Text, ((ListViewItem)y).SubItems[_column].Text);
        }
    }
}