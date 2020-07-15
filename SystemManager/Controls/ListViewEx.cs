using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PluginInterface;

namespace SM.Controls
{
    public partial class ListViewEx : ListView
    {
        // removes dotted line around focused item
        //public bool ShowFocusCues { get; set; }
        public ListViewItem FocusedItemKeyDown;
        private int _boxWidth = 16;
        private int LeftIconOffset => SmallImageList.ImageSize.Width + _boxWidth;
        ListViewItem _oldListViewItem;

        public ListViewEx()
        {
            InitializeComponent();
            DoubleBuffered = true; // no flicking
            ListViewItemSorter = new ListViewItemComparer();

            Resize += ListViewEx_Resize;
            ColumnClick += ListView_ColumnClick;
            KeyDown += ListView_KeyDown;
            KeyUp += ListView_KeyUp;
            //base.Resize += (sender, e) => { IsUpdating = true; };
            base.SizeChanged += (sender, e) =>
            {
                //IsUpdating = false;
            };

            //base.Invalidated += ((sender, e) => { IsUpdating = true; });
            //base.Validated += ((sender, e) => { IsUpdating = false; });
            base.MouseMove += ListView_MouseMove;
            base.MouseDown += ListView_MouseDown;
            base.ItemChecked += (sender, args) =>
            {
                var listView = sender as ListView;
                if (args.Item.Checked)
                    args.Item.ForeColor = Color.Red;
                else
                    args.Item.ForeColor = Color.Black;
            };
        }

        private void ListViewEx_Resize(object sender, System.EventArgs e)
        {
            //UpdateColumnHeaders();

            //if (sender is ListViewEx listView)
            //{
            //    int x = listView.Width / 100;
            //    listView.Columns[0].Width = x * 68;
            //    listView.Columns[1].Width = x * 5;
            //    listView.Columns[2].Width = x * 10;
            //    listView.Columns[3].Width = x * 12;
            //    listView.Columns[4].Width = x * 5;
            //}
        }
        // Draws the backgrounds for entire ListView items.
        private void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var listView = sender as ListView;

            //Logger.Info("DrawItem: " + e.Item.Name + "   " + e.State);

            //if (IsUpdating)
            //{
            //    return;
            //}
            //if (this.View == View.Details)
            //{
            //    e.DrawDefault = false;
            //}

            /*  Checked 	        8 	    The item is checked.
                Default 	        32 	    The item is in its default state.
                Focused 	        16 	    The item has focus.
                Grayed 	            2       The item is disabled.
                Hot 	            64 	    The item is currently under the mouse pointer.
                Indeterminate       256 	The item is in an indeterminate state.
                Marked              128     The item is marked.
                Selected            1       The item is selected.
                ShowKeyboardCues    512 The item should indicate a keyboard shortcut.
             */

            // Draw the background and focus rectangle for a selected item.
            if (e.State.HasFlag(ListViewItemStates.Focused))
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
                //e.DrawFocusRectangle();
            }
            else if (e.Item.Selected)
            {
                //e.Graphics.FillRectangle(Brushes.Gray, e.Bounds);
                //e.DrawFocusRectangle();
            }

            if (listView.CheckBoxes)
            {
                if (e.Item.Checked)
                {
                    e.Graphics.FillRectangle(Brushes.Black, e.Bounds.X, e.Bounds.Y, e.Bounds.X + _boxWidth, e.Bounds.Y);
                }
                else if (!e.Item.Checked)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds.X, e.Bounds.Y, e.Bounds.X + _boxWidth, e.Bounds.Y);
                }
                e.Graphics.DrawImage(SmallImageList.Images[e.Item.ImageIndex], new Point(e.Bounds.Location.X + _boxWidth, e.Bounds.Y));
            }
        }

        // Draws subitem text and applies content-based formatting.
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //bug, known since 2006, when the ListView.HideSelection property is set to FALSE e.State is always true. Use e.Item.Selected instead.
            var listView = sender as ListView;

            // Logger.Info("DrawSubItem: " + e.Item.Name);

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sf.LineAlignment = StringAlignment.Center;
                //e.Graphics.DrawString(e.SubItem.Text, listView.Font, new SolidBrush(e.Item.ForeColor), e.Bounds, sf);

                //if ((e.ItemState & ListViewItemStates.Selected) == 0)
                //{
                //    e.DrawBackground();
                //}

                if (e.ColumnIndex > 0)
                {
                    // Unless the item is selected, draw the standard 
                    // background to make it stand out from the gradient.
                    // Draw the subitem text in red to highlight it. 
                    e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Green, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    if (e.Item.Checked)
                    {
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Red, new Rectangle(e.Bounds.X + LeftIconOffset, e.Bounds.Y, e.Bounds.Width - LeftIconOffset, e.Bounds.Height), sf);
                    }
                    else
                    {
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Black, new Rectangle(e.Bounds.X + LeftIconOffset, e.Bounds.Y, e.Bounds.Width - LeftIconOffset, e.Bounds.Height), sf);
                    }
                }
            }
        }

        // Draws column headers.
        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Helvetica", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
            return;
        }
        // Resets the item tags.
        private void ListView_Invalidated(object sender, InvalidateEventArgs e)
        {
            var listView = sender as ListViewEx;
            //foreach (ListViewItem item in listView.Items)
            //{
            //    if (item == null) return;
            //    item.Tag = null;
            //}
        }

        // Forces the entire control to repaint if a column width is changed.
        void ListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            var listView = sender as ListView;
            //listView.Invalidate();
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //ListView listView = sender as ListView;
            ListViewItemComparer listViewItemSorter = ListViewItemSorter as ListViewItemComparer;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == listViewItemSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                listViewItemSorter.SortOrder = listViewItemSorter.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                listViewItemSorter.SortColumn = e.Column;
                listViewItemSorter.SortOrder = SortOrder.Ascending;
            }

            //// Perform the sort with these new sort options.
            Sort();
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            FocusedItemKeyDown = FocusedItem;

            // Check selected items when Shift Up/Down
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    FocusedItem.Checked = !FocusedItem.Checked;
                }
            }

            //Check all items
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Add)
                {
                    foreach (ListViewItem item in Items.Cast<ListViewItem>())
                    {
                        item.Checked = true;
                    }
                }
                //Uncheck all items
                if (e.KeyCode == Keys.Subtract)
                {
                    foreach (ListViewItem item in CheckedItems.Cast<ListViewItem>())
                    {
                        item.Checked = false;
                    }
                }
                //Invert all items
                if (e.KeyCode == Keys.Multiply)
                {
                    foreach (ListViewItem item in Items.Cast<ListViewItem>())
                    {
                        item.Checked = !item.Checked;
                    }
                }
            }
        }

        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListViewEx;

            // Check selected items when Shift PageUp or PageDown is pressed
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    bool flag = !listView.FocusedItemKeyDown.Checked;
                    int endItemIndex = listView.FocusedItem.Index;
                    var startItemIndex = listView.FocusedItemKeyDown.Index;

                    if (endItemIndex > startItemIndex)
                    {
                        for (int i = startItemIndex; i < endItemIndex; i++)
                        {
                            listView.Items[i].Checked = flag;
                        }
                    }
                    else
                    {
                        for (int i = endItemIndex + 1; i <= startItemIndex; i++)
                        {
                            listView.Items[i].Checked = flag;
                        }
                    }
                }
            }
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;

            if (e.Button == MouseButtons.Right)
            {
                ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;

                if (item != null && item != _oldListViewItem)
                {
                    item.Checked = !item.Checked;
                    _oldListViewItem = item;
                }
            }
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (e.Button == MouseButtons.Right)
            {
                if (item != null && item != _oldListViewItem)
                {
                    if (_oldListViewItem != null)
                        _oldListViewItem.Selected = false;

                    _oldListViewItem = item;
                    item.Checked = !item.Checked;
                    item.Selected = true;
                }
            }
        }
        public void UpdateColumnHeaders(IEnumerable<Tag> headers)
        {
            SuspendLayout();
            //ListViewItemSorter  = null;

            int columns = Columns.Count;
            for (int i = 0; i < columns; i++)
            {
                Columns.RemoveAt(0);
            }

            // Add new columns
            foreach (Tag header in headers)
            {
                var columnHeader = new ColumnHeader();
                columnHeader.Width = (int)(ClientSize.Width * header.Width / 100.0f);
                columnHeader.Text = header.Name;
                Columns.Add(columnHeader);
            }

            //ListViewItemSorter = new ListViewColumnSorter();
            ResumeLayout();
        }

        public void ClearSelections()
        {
            foreach (ListViewItem listViewItem in Items)
            {
                listViewItem.Selected = false;
            }
        }

        /// <summary>
        /// Move the selected focused item up or down.
        /// </summary>
        /// <param name="index">Number of step to move selection from current position. Negative=Up, Positive=Down</param>
        /// <returns>True is successful.</returns>
        public bool ChangeSelectedItem(int index)
        {
            int currentIndex = FocusedItem.Index;
            if ((index < 0 && currentIndex + index > 0) || (index > 0 && currentIndex + index < Items.Count))
            {
                ClearSelections();
                FocusedItem = Items[currentIndex + index];
                Items[currentIndex + index].Selected = true;
                EnsureVisible(currentIndex + index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Move the selected focused up or down based on index and item search.
        /// </summary>
        /// <param name="index"> Index>0 searches next items upward. Index<0 searches next item downward.</param>
        /// <param name="search">Search string for match with beginning of the item text.</param>
        /// <param name="trimChars"></param>
        /// <returns>True is successful.</returns>
        public bool ChangeSelectedItem(int index, string search, char[] trimChars = null)
        {
            index = index > 0 ? Math.Max(index, 1) : Math.Min(index, -1);
            int currentIndex = FocusedItem.Index;

            if (index > 0 && currentIndex + index < Items.Count)// look downward in the list
            {
                for (int i = currentIndex + 1; i < Items.Count; i++)
                {
                    if (Items[i].Text.TrimStart(trimChars).StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    {
                        ClearSelections();
                        FocusedItem = Items[i];
                        Items[i].Selected = true;
                        EnsureVisible(i);
                        return true;
                    }
                }
            }
            else if (index < 0 && currentIndex + index > 0) // look upward in the list
            {
                for (int i = currentIndex - 1; i > 0; i--)
                {
                    if (Items[i].Text.TrimStart(trimChars).StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    {
                        ClearSelections();
                        FocusedItem = Items[i];
                        Items[i].Selected = true;
                        EnsureVisible(i);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Find first ListViewItem.Text  matching search string.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="startIndex">Starting index for search.</param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public ListViewItem FindItemWithTextEx(string search, int startIndex, char[] trimChars = null)
        {
            if (startIndex < Items.Count)
            {
                for (int i = startIndex; i < Items.Count; i++)
                {
                    if (Items[i].Text.TrimStart(trimChars).StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Items[i];
                    }
                }
            }

            if (startIndex > 0)
            {
                for (int i = 0; i < startIndex; i++)
                {
                    if (Items[i].Text.TrimStart(trimChars).StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Items[i];
                    }
                }
            }

            return null;
        }
    }
}