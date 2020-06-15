using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SM.Controls
{
    public partial class ListViewEx : ListView
    {
        public ListViewItem FocusedItemKeyDown;
        private int boxWidth = 16;
        private int leftIconOffset => SmallImageList.ImageSize.Width + boxWidth;

        public bool IsUpdating { get; private set; }

        //protected override void OnNotifyMessage(Message m)
        //{
        //    //Filter out the WM_ERASEBKGND message
        //    if (m.Msg != 0x14)
        //    {
        //        base.OnNotifyMessage(m);
        //    }
        //}
        public ListViewEx()
        {

            ////Activate double buffering
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            ////Enable the OnNotifyMessage event so we get a chance to filter out 
            //// Windows messages before they get to the form's WndProc
            //this.SetStyle(ControlStyles.EnableNotifyMessage, true);


            InitializeComponent();
            ListViewItemSorter = new ListViewColumnSorter();
            //DrawItem += ListView_DrawItem;
            //DrawColumnHeader += ListView_DrawColumnHeader;
            //DrawSubItem += ListView_DrawSubItem;
            Resize += ListViewEx_Resize;

            //leftListView.Invalidated += ListView_Invalidated;
            //ColumnWidthChanged += ListView_ColumnWidthChanged;
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
        }

        private void ListViewEx_Resize(object sender, System.EventArgs e)
        {
            return;
            
            if (sender is ListViewEx listView)
            {
                int x = listView.Width / 100;
                listView.Columns[0].Width = x * 68;
                listView.Columns[1].Width = x * 5;
                listView.Columns[2].Width = x * 10;
                listView.Columns[3].Width = x * 12;
                listView.Columns[4].Width = x * 5;
            }
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
                    e.Graphics.FillRectangle(Brushes.Black, e.Bounds.X, e.Bounds.Y, e.Bounds.X + boxWidth, e.Bounds.Y);
                }
                else if (!e.Item.Checked)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds.X, e.Bounds.Y, e.Bounds.X + boxWidth, e.Bounds.Y);
                }
                e.Graphics.DrawImage(SmallImageList.Images[e.Item.ImageIndex], new Point(e.Bounds.Location.X + boxWidth, e.Bounds.Y));
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
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Red, new Rectangle(e.Bounds.X + leftIconOffset, e.Bounds.Y, e.Bounds.Width - leftIconOffset, e.Bounds.Height), sf);
                    }
                    else
                    {
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Black, new Rectangle(e.Bounds.X + leftIconOffset, e.Bounds.Y, e.Bounds.Width - leftIconOffset, e.Bounds.Height), sf);
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
            ListView listView = sender as ListView;
            ListViewColumnSorter listViewItemSorter = listView.ListViewItemSorter as ListViewColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == listViewItemSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (listViewItemSorter.Order == SortOrder.Ascending)
                {
                    listViewItemSorter.Order = SortOrder.Descending;
                }
                else
                {
                    listViewItemSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                listViewItemSorter.SortColumn = e.Column;
                listViewItemSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listView.Sort();

            //ItemContainer itemContainer = listView.Tag as ItemContainer;
            //UpdateListBox(listView, itemContainer, e.Column);
            //Logger.Debug(sender);
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

            // Check selected items when Shift Up/Down
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
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
