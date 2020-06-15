using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Transactions;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemManager.EventAggregator;
using System.Threading;
using System.DirectoryServices.ActiveDirectory;
using SystemManager.Event;
using NLog;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SystemManager
{
    public partial class SystemManager : Form
    {
        private Subscription<Event.CopyDialogEventArgs> copyDialogSubscription;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        //private void ErrorArgs(Event.LogMessageArgs errorArgs)
        //{
        //    Logger.Error(DateTime.Now + ": " + errorArgs.Message + Environment.NewLine);
        //}

        private SenderSide senderSide { get; set; }
        private ListView currentListView { get; set; }
        private TextBox currentStatusTextBox { get; set; }
        public enum SenderSide
        {
            Left,
            Right
        }

        public SystemManager()
        {
            InitializeComponent();
            leftListView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            leftListView.MouseDown += new MouseEventHandler(ListView_MouseDown);
            leftListView.Resize += new EventHandler(ListView_Resize);
            leftListView.KeyDown += new KeyEventHandler(ListView_KeyDown);
            leftListView.GotFocus += ListView_GotFocus;
            leftListView.ColumnClick += ListView_ColumnClick;
            leftListView.ListViewItemSorter = new ListViewColumnSorter();

            leftListView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            leftListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            leftListView.ItemCheck += ListView_ItemCheck;
            leftListView.ItemChecked += ListView_ItemChecked;
            leftListView.MouseMove += ListView_MouseMove;
            leftListView.MouseUp += ListView_MouseUp;

            leftListView.OwnerDraw = true;
            leftListView.DrawItem += ListView_DrawItem;
            leftListView.DrawColumnHeader += ListView_DrawColumnHeader;
            leftListView.DrawSubItem += ListView_DrawSubItem;
            //leftListView.Invalidated += ListView_Invalidated;
            leftListView.ColumnWidthChanged += ListView_ColumnWidthChanged;
            leftListView.KeyUp += ListView_KeyUp;
            //leftListView.BackColor = Color.Black;
            //leftListView.ForeColor = Color.White;


            rightListView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            rightListView.MouseDown += new MouseEventHandler(ListView_MouseDown);
            rightListView.Resize += new EventHandler(ListView_Resize);
            rightListView.KeyDown += new KeyEventHandler(ListView_KeyDown);
            rightListView.GotFocus += ListView_GotFocus;
            rightListView.ColumnClick += ListView_ColumnClick;
            rightListView.ListViewItemSorter = new ListViewColumnSorter();

            //rightListView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            //leftListView.KeyPress += new KeyPressEventHandler(LeftlistView_KeyPress);
            //leftListView.ItemActivate += new EventHandler(ListView_ItemActivate);

            this.WindowState = FormWindowState.Maximized;
            this.Text = Application.ProductName + @" " + Application.ProductVersion;
        }
        public ListViewItem FocusedItemKeyDown;

        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            // FocusedItemKeyUp = listView.FocusedItem;

            // Check selected items when Shift Up/Down
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    listView.FocusedItem.Checked = !listView.FocusedItem.Checked;
                }
                if (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    bool flag = !FocusedItemKeyDown.Checked;
                    int endItemIndex = listView.FocusedItem.Index;
                    var startItemIndex = FocusedItemKeyDown.Index;

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

        // Forces the entire control to repaint if a column width is changed.
        void ListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            var listView = sender as ListView;
            //listView.Invalidate();
        }

        // Resets the item tags.
        private void ListView_Invalidated(object sender, InvalidateEventArgs e)
        {
            var listView = sender as ListView;
            //foreach (ListViewItem item in listView.Items)
            //{
            //    if (item == null) return;
            //    item.Tag = null;
            //}
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            FocusedItemKeyDown = listView.FocusedItem;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            ItemContainer itemContainer = listView.FocusedItem.Tag as ItemContainer;
                            if (itemContainer.IsDirectory)
                                UpdateListBox(listView, itemContainer);
                            break;
                        }
                    case Keys.Back:
                        {
                            ItemContainer itemContainer = listView.Tag as ItemContainer;
                            //No need to update if we already are in root folder.
                            if (!itemContainer.IsRoot)
                            {
                                UpdateListBox(listView, itemContainer.Parent);
                            }
                            break;
                        }
                    case Keys.F5:
                        {
                            StartCopyOperation();
                            break;
                        }
                    case Keys.Escape:
                        {
                            logRichTextBox.Clear();
                            break;
                        }
                }

                // Check selected items when Shift Up/Down
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                    {
                        listView.FocusedItem.Checked = !listView.FocusedItem.Checked;
                    }
                    if (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                    {
                        int end = listView.FocusedItem.Index;
                        int top = leftListView.TopItem.Index;
                        var s = leftListView.SelectedItems[0].Index;
                        var aas = leftListView.SelectedItems;

                        for (int i = s; i < (s + 10); i++)
                        {
                            // listView.Items[i].Checked = true;//listView.FocusedItem.Checked;
                        }

                        //foreach (ListViewItem listViewSelectedItem in listView.SelectedItems)
                        //{
                        //    listViewSelectedItem.Checked = listView.FocusedItem.Checked;
                        //}
                    }
                }

                //Check all items
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (e.KeyCode == Keys.A || e.KeyCode == Keys.Add)
                    {
                        foreach (ListViewItem item in listView.Items.Cast<ListViewItem>())
                        {
                            item.Checked = item.Checked;
                        }
                    }
                }

                //Uncheck all items
                if (e.KeyCode == Keys.Control | e.KeyCode == Keys.Subtract)
                {
                    foreach (ListViewItem item in listView.CheckedItems.Cast<ListViewItem>())
                    {
                        item.Checked = false;
                    }
                }
                //Invert all items
                if (e.KeyCode == Keys.Control | e.KeyCode == Keys.Multiply)
                {
                    foreach (ListViewItem item in listView.Items.Cast<ListViewItem>())
                    {
                        item.Checked = !item.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        // Draws the backgrounds for entire ListView items.
        private void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var listView = sender as ListView;

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

            if (listView.CheckBoxes && e.Item.Checked)
            {
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds.X, e.Bounds.Y, e.Bounds.X + 16, e.Bounds.Y + 16);
            }
            else if (listView.CheckBoxes && !e.Item.Checked)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds.X, e.Bounds.Y, e.Bounds.X + 16, e.Bounds.Y + 16);
            }

            //else if 
            //{
            //    // Draw the background for an unselected item.
            //    e.Graphics.FillRectangle(Brushes.Gray, e.Bounds);
            //}
            //Logger.Error(counter + ": " + e.Item.Name + "  " + e.State);

            //if (e.State == ListViewItemStates.Focused)
            //{
            //    e.DrawFocusRectangle();
            //    e.Graphics.FillRectangle(Brushes.Red, e.Bounds);

            //    // Draw the background and focus rectangle for a selected item.
            //    //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //    //e.DrawFocusRectangle();
            //}
            //if (e.State == ListViewItemStates.Selected)
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //}
            //Logger.Error(e.State);
            //if ((e.State & ListViewItemStates.Selected) == 0)
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //}
            //else if ((e.State & ListViewItemStates.Focused) == 0)
            //{
            //    e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
            //}
            //if (e.State == ListViewItemStates.Hot)
            //{
            //    e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
            //}

            ////if ((e.State & ListViewItemStates.Selected) != 0)
            //if (e.State != ListViewItemStates.Selected)
            //{
            //    // Draw the background and focus rectangle for a selected item.
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //    e.DrawFocusRectangle();
            //}
            //else if (e.State == ListViewItemStates.Selected)
            //{
            //    using Brush brush = new SolidBrush(Color.Gray);
            //    e.Graphics.FillRectangle(brush, e.Bounds);
            //}

            //// Draw the item text for views other than the Details view.
            //if (listView.View != View.Details)
            //{
            //    e.DrawText();
            //}
        }

        // Draws subitem text and applies content-based formatting.
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //bug, known since 2006, when the ListView.HideSelection property is set to FALSE e.State is always true. Use e.Item.Selected instead.

            var listView = sender as ListView;
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                //e.Graphics.DrawString(e.SubItem.Text, listView.Font, new SolidBrush(e.Item.ForeColor), e.Bounds, sf);

                if (e.ColumnIndex > 0)
                {
                    // Unless the item is selected, draw the standard 
                    // background to make it stand out from the gradient.
                    //if ((e.ItemState & ListViewItemStates.Selected) == 0)
                    //{
                    //    e.DrawBackground();
                    //}

                    // Draw the subitem text in red to highlight it. 
                    e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Green, e.Bounds, sf);
                }

                if (e.ColumnIndex == 0)
                {
                    if (e.Item.Checked)
                    {
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Red,
                            new Rectangle(e.Bounds.X + 32, e.Bounds.Y, e.Bounds.Width - 32, e.Bounds.Height), sf);
                    }
                    else
                    {
                        e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.Black,
                            new Rectangle(e.Bounds.X + 32, e.Bounds.Y, e.Bounds.Width - 32, e.Bounds.Height), sf);
                    }
                }
                // Store the column text alignment, letting it default to Left if it has not been set to Center or Right.
                //switch (e.Header.TextAlign)
                //{
                //    case HorizontalAlignment.Center:
                //        sf.Alignment = StringAlignment.Center;
                //        flags = TextFormatFlags.HorizontalCenter;
                //        break;
                //    case HorizontalAlignment.Right:
                //        sf.Alignment = StringAlignment.Far;
                //        flags = TextFormatFlags.Right;
                //        break;
                //}

                // Draw the text and background for a subitem with a negative value. 
                //double subItemValue;
                //if (e.ColumnIndex > 0 && Double.TryParse(e.SubItem.Text, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out subItemValue) && subItemValue < 0)
                //{
                //    // Unless the item is selected, draw the standard background to make it stand out from the gradient.
                //    if ((e.ItemState & ListViewItemStates.Selected) == 0)
                //    {
                //        e.DrawBackground();
                //    }

                //    // Draw the subitem text in red to highlight it. 
                //    e.Graphics.DrawString(e.SubItem.Text, listView.Font, Brushes.White, e.Bounds, sf);

                //    return;
                //}

                // Draw normal text for a subitem with a nonnegative or nonnumerical value.
                //e.DrawText(TextFormatFlags.Left);
                //if (!MouseStatus)
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
        // Selects and focuses an item when it is clicked anywhere along 
        // its width. The click must normally be on the parent item text.
        private void ListView_MouseUp(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;
            //ListViewItem clickedItem = listView.GetItemAt(e.X, e.Y);
            //if (clickedItem != null)
            //{
            //clickedItem.Selected = true;
            //clickedItem.Focused = true;
            //MouseStatus = false;
            //}
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var listView = sender as ListView;

            //ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            //ListViewItem item = info.Item;

            //if (item != null)
            //{
            //    ItemContainer itemContainer = item.Tag as ItemContainer;
            //    if (itemContainer.IsDirectory)
            //        UpdateListBox(listView, itemContainer);
            //}
            //else
            //{
            //    listView.SelectedItems.Clear();
            //    //EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs("No Item is selected."));
            //}
        }

        ListViewItem oldListViewItem;
        bool mouseStatus = false;
        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;

            if (e.Button == MouseButtons.Right)
            {
                mouseStatus = true;
                ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;

                if (item != null)
                {
                    if (item != oldListViewItem)
                    {
                        //item.ForeColor = Color.Red;
                        //listView.RedrawItems(item.Index, item.Index + 1, true);
                        //listView.Invalidate(item.GetBounds(ItemBoundsPortion.Entire));
                        //item.ForeColor = Color.Red;
                        //oldListViewItem = item;
                        //item.Checked = !item.Checked;
                        //item.Selected = item.Checked;
                    }
                }
            }
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            //ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            //ListViewItem item = info.Item;

            //if (item != null && item.Tag == null)
            //{
            //listView.Invalidate(item.Bounds);
            //item.Tag = "tagged";
            //}
            if (e.Button == MouseButtons.Right)
            {
                // Forces each row to repaint itself the first time the mouse moves over 
                // it, compensating for an extra DrawItem event sent by the wrapped 
                // Win32 control. This issue occurs each time the ListView is invalidated.
                //if (item != null)
                //{
                //    if (item != oldListViewItem)
                //    {
                //oldListViewItem = item;
                //item.Checked = !item.Checked;
                //item.Selected = true;
                //}
                //}
            }
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //ListView listView = sender as ListView;
            //if (e.Item.Checked)
            //{
            //    e.Item.ForeColor = Color.Red;
            //}
            //else
            //{
            //    e.Item.ForeColor = DefaultForeColor;
            //}
        }

        private void ListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListView listView = sender as ListView;
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            //if (e.IsSelected)
            //{
            //    Logger.Debug("ListView_ItemSelectionChanged");
            //}
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            //Logger.Debug("ListView_SelectedIndexChanged");
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

        private void ListView_GotFocus(object sender, EventArgs e)
        {
            currentListView = sender as ListView;
            ItemContainer itemContainer = currentListView.Tag as ItemContainer;

            if (currentListView.Name.Contains("left"))
            {
                currentStatusTextBox = textBoxStatus1;
                //logTextBox.Text = itemContainer.Path.ShrinkPath(80);
                //textBoxCmd.Text = 
            }
            else
            {
                currentStatusTextBox = textBoxStatus2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] drives = Directory.GetLogicalDrives();

            //Create volume buttons
            //TODO: detect changes to new attached or detached volumes
            for (int i = 0; i < 2; i++)
            {
                foreach (string drive in drives)
                {
                    Button buttom = new Button
                    {
                        Text = drive,
                        Size = new System.Drawing.Size(75, 35),
                        Anchor = AnchorStyles.Left,
                        TabStop = false
                    };

                    if (i == 0)
                    {
                        buttom.Tag = SenderSide.Left.ToString();
                        leftFlowLayoutPanel.Controls.Add(buttom);
                    }
                    else
                    {
                        buttom.Tag = SenderSide.Right.ToString();
                        rightFlowLayoutPanel.Controls.Add(buttom);
                    }

                    buttom.Click += VolumeButtom_Click;
                }
            }

            FileSystemContainer fileSystemContainter = new FileSystemContainer(@"C:\");

            //TODO: intrim solution just init both listviews
            currentListView = rightListView;
            rightListView.Tag = fileSystemContainter;
            UpdateListBox(rightListView, fileSystemContainter);

            currentListView = leftListView;
            leftListView.Tag = fileSystemContainter;
            UpdateListBox(leftListView, fileSystemContainter);
        }

        private void VolumeButtom_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            string volumePath = button.Text;
            var fileSystemContainter = new FileSystemContainer(volumePath);

            string senderSide = button.Tag as string;
            if (senderSide == SenderSide.Left.ToString())
            {
                UpdateListBox(leftListView, fileSystemContainter);
            }
            else
            {
                UpdateListBox(rightListView, fileSystemContainter);
            }
        }

        private void UpdateListBox(ListView listView, ItemContainer newItemContainer, int sortIndex = 0)
        {
            ItemContainer oldItemContainer = listView.Tag as ItemContainer;
            listView.Tag = newItemContainer;

            listView.BeginUpdate();
            listView.Items.Clear();

            EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs("Try to open " + newItemContainer.Path));

            //create a list of items and add them to current ListView

            // GetItems ones. Each GetItems trigger filesystem get files and directory.
            List<ItemContainer> newItemContainerItems = newItemContainer.GetItems().ToList();

            var dirs = newItemContainerItems.Where(x => x.IsDirectory);
            var files = newItemContainerItems.Where(x => x.IsFile).ToList();
            files.Sort((a, b) => String.Compare(a.Attributes[0], b.Attributes[sortIndex], StringComparison.Ordinal));
            //var files = newItemContainerItems.Where(x=>x.IsFile).OrderBy(x => x.Attributes[0], StringComparer.CurrentCultureIgnoreCase).ToList();

            newItemContainerItems = new List<ItemContainer>();
            newItemContainerItems.AddRange(dirs);
            newItemContainerItems.AddRange(files);

            foreach (ItemContainer itemContainer in newItemContainerItems)
            {
                var listViewItem = new ListViewItem(itemContainer.Name, 0)
                {
                    Tag = itemContainer,
                    Text = itemContainer.Name,
                    Name = itemContainer.Path,
                    ImageIndex = itemContainer.IsDirectory ? 0 : 1 // Name is used as key in ListViewItem, we are using Path on each item since it's unique
                };
                // set correct image based on folder and file

                //Add extra column info such as dir, size attr
                foreach (string attribute in itemContainer.Attributes)
                {
                    listViewItem.SubItems.Add(attribute);
                }

                listView.Items.Add(listViewItem);
            }

            //Focus on right ListViewItem
            ListViewItem focusListViewItem = listView.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == oldItemContainer?.Name);
            if (focusListViewItem == null)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
            }
            else
            {
                // When we navigate back, we want to focus on right ListViewItem
                listView.Items[oldItemContainer?.Path].Selected = true;
                listView.Items[oldItemContainer?.Path].Focused = true;
            }
            currentListView.EndUpdate();

            int countDir = newItemContainerItems.Count(x => x.IsDirectory && !x.IsParent);
            int countFiles = newItemContainerItems.Count(x => x.IsFile);
            EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs(countFiles + " files and " + countDir + " directories listed."));
        }
        private void ListView_Resize(object sender, EventArgs e)
        {
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

        private void StartCopyOperation()
        {
            copyDialogSubscription = EventAggregator.EventAggregator.Instance.Subscribe<Event.CopyDialogEventArgs>(this.HandleCopyDialogResults);

            FormComfirmCopy formCopy = new FormComfirmCopy();
            formCopy.ShowDialog();
        }
        private async void HandleCopyDialogResults(Event.CopyDialogEventArgs copyEventAgrs)
        {
            if (copyEventAgrs.ConfirmationDialog == Event.ConfirmationDialog.OK)
            {
                FormProgress formProgress = new FormProgress();
                formProgress.Show();

                IEnumerable<string> selectedDirectories = currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as ItemContainer).Path);

                CancellationToken cancellationToken = new CancellationToken(false);
                await GetDirectoryAggregatorInfo(selectedDirectories, cancellationToken).ConfigureAwait(true);

                EventAggregator.EventAggregator.Instance.UnSubscribe(copyDialogSubscription);

                Event.CopyProgressEventArgs copyProgressEventArgs = new Event.CopyProgressEventArgs();
                copyProgressEventArgs.OperationStatus = Event.OperationStatus.Initiated;
                EventAggregator.EventAggregator.Instance.Publish(copyProgressEventArgs);

                string[] directories = currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as ItemContainer).Path).ToArray();
                Logger.Info("Copy: " + "Operation started.");
                await Task.Run(() => HelperIO.DirectoryCopy(directories, @"R:\", true, true));
                Logger.Info("Copy: " + "Operation ended successfully.");
            }
        }

        public struct DirectoryAggregatorInfo
        {
            public long Count { get; set; }
            public long Size { get; set; }
        }
        /// <summary>
        /// Get number of files and total size
        /// </summary>
        /// <param name="countFiles"></param>
        /// <param name="countSize"></param>
        private static async Task GetDirectoryAggregatorInfo(IEnumerable<string> items, CancellationToken cancellationToken)
        {
            DirectoryAggregatorInfo directoryAggregatorInfo = new DirectoryAggregatorInfo();
            try
            {
                foreach (string item in items)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    long countFiles = 0, countSize = 0;

                    int publishInterval = 0;
                    //TODO support Cancel
                    foreach (var fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
                    {
                        countFiles++;
                        countSize += fileInfo.Length;

                        publishInterval = publishInterval < 1001 ? ++publishInterval : 0;
                        if (publishInterval > 1000)
                        {
                            EventAggregator.EventAggregator.Instance.Publish<Event.FileCountArgs>(new Event.FileCountArgs(countFiles, countSize));
                        }
                    }
                    EventAggregator.EventAggregator.Instance.Publish<Event.FileCountArgs>(new Event.FileCountArgs(countFiles, countSize));
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                Logger.Error(ex.Message);
                //EventAggregator.EventAggregator.Instance.Publish<Event.LogMessageArgs>(new Event.LogMessageArgs(ex.Message));
            }
        }
    }
}
