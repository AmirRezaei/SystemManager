using System.Collections;
using System.Windows.Forms;
using PluginInterface;

namespace SM.Controls
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private readonly CaseInsensitiveComparer _objectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewItemComparer()
        {
            // Initialize the column to '0'
            //_columnToSort = 0;

            // Initialize the sort order to 'none'
            SortOrder = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            _objectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            // Cast the objects to be compared to ListViewItem objects
            var listViewX = x as ListViewItem;
            var listViewY = y as ListViewItem;

            // Do not sort Directory, only files
            var entity = listViewX.Tag as Entity;
            if (entity.IsDirectory)
                return 0;

            // Compare the two items
            int compareResult = _objectCompare.Compare(listViewX.SubItems[SortColumn].Text, listViewY.SubItems[SortColumn].Text);

            // Calculate correct return value based on object comparison
            if (SortOrder == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }

            if (SortOrder == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }

            // Return '0' to indicate they are equal
            return 0;
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn { get; set; }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder SortOrder { get; set; }
    }
}
