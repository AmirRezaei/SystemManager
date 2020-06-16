using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper.EventAggregator;
using PluginInterface;
using ProcessPlugin;
using ServicePlugin;
using Helper;
using MediaMetaTagPlugin;
using SM.Controls;
using SM.EventTypes;

namespace SM
{
    public partial class SystemManager : Form
    {
        private Subscription<global::SM.EventTypes.CopyDialogEventArgs> _copyDialogSubscription;
        private static NLog.Logger _logger;

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
            leftListView.MouseDoubleClick += ListView_MouseDoubleClick;
            leftListView.MouseDown += ListView_MouseDown;
            leftListView.GotFocus += ListView_GotFocus;
            leftListView.MouseMove += ListView_MouseMove;
            leftListView.KeyDown += ListView_KeyDown;

            rightListView.MouseDoubleClick += ListView_MouseDoubleClick;
            rightListView.MouseDown += ListView_MouseDown;
            rightListView.KeyDown += ListView_KeyDown;
            rightListView.GotFocus += ListView_GotFocus;

            this.WindowState = FormWindowState.Maximized;
            this.Text = Application.ProductName + @" " + Application.ProductVersion;
        }


        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            Entity itemContainer = listView.FocusedItem.Tag as Entity;
                            if (itemContainer.IsDirectory)
                                UpdateListBox(listView, itemContainer);
                            break;
                        }
                    case Keys.Back:
                        {
                            Entity itemContainer = listView.Tag as Entity;
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
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
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
            var listView = sender as ListView;

            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                Entity itemContainer = item.Tag as Entity;
                if (itemContainer.IsDirectory)
                    UpdateListBox(listView, itemContainer);
            }
            else
            {
                listView.SelectedItems.Clear();
                //EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs("No Item is selected."));
            }
        }

        ListViewItem oldListViewItem;
        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;

            if (e.Button == MouseButtons.Right)
            {
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

        private void ListView_GotFocus(object sender, EventArgs e)
        {
            currentListView = sender as ListView;
            Entity itemContainer = currentListView.Tag as Entity;

            if (currentListView.Name.Contains("left"))
            {
                currentStatusTextBox = textBoxStatus1;
            }
            else
            {
                currentStatusTextBox = textBoxStatus2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDriveButtons();
            //Init logger after form has been loaded and to ensure logRichTextBox is initiated.
            _logger = NLog.LogManager.GetCurrentClassLogger();

            // Force resize splitContainer1 to 50%
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = false;
            splitContainer1.SplitterDistance = (int)(splitContainer1.ClientSize.Width * 0.50);
        }

        private void CreateDriveButtons()
        {
            string[] drives = Directory.GetLogicalDrives();

            //Create volume buttons
            //TODO: detect changes to new attached or detached volumes
            for (int i = 0; i < 2; i++)
            {
                foreach (string drive in drives)
                {
                    Button button = new Button
                    {
                        Text = drive,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top,
                        AutoSize = true,
                        TabStop = false
                    };

                    if (i == 0)
                    {
                        button.Tag = SenderSide.Left.ToString();
                        leftFlowLayoutPanel.Controls.Add(button);
                    }
                    else
                    {
                        button.Tag = SenderSide.Right.ToString();
                        rightFlowLayoutPanel.Controls.Add(button);
                    }

                    button.Click += VolumeButton_Click;
                }
            }

            FileSystemEntity fileSystemContainter = new FileSystemEntity(@"C:\");

            //TODO: intrim solution just init both listviews
            currentListView = rightListView;
            rightListView.Tag = fileSystemContainter;

            var entities = fileSystemContainter.GetEntities();
            var entity = entities.FirstOrDefault();

            UpdateColumnHeaders(rightListView, entity?.Attributes.Keys.ToArray());
            UpdateListBox(rightListView, fileSystemContainter);

            currentListView = leftListView;
            leftListView.Tag = fileSystemContainter;
            UpdateColumnHeaders(leftListView, entity?.Attributes.Keys.ToArray());
            UpdateListBox(leftListView, fileSystemContainter);
        }

        private void VolumeButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            string volumePath = button.Text;
            var fileSystemContainter = new FileSystemEntity(volumePath);

            string senderSide = button.Tag as string;
            if (senderSide == SystemManager.SenderSide.Left.ToString())
            {
                var entities = fileSystemContainter.GetEntities();
                var entity = entities.FirstOrDefault();

                UpdateColumnHeaders(leftListView, entity?.Attributes.Keys.ToArray());
                UpdateListBox(leftListView, fileSystemContainter);
            }
            else
            {
                var entities = fileSystemContainter.GetEntities();
                var entity = entities.FirstOrDefault();

                UpdateColumnHeaders(rightListView, entity?.Attributes.Keys.ToArray());
                UpdateListBox(rightListView, fileSystemContainter);
            }
        }

        private void UpdateListBox(ListView listView, Entity newItemContainer, int sortIndex = 0)
        {
            if (newItemContainer == null)
                return;

            Entity oldItemContainer = listView.Tag as Entity;
            listView.Tag = newItemContainer;

            listView.SuspendLayout();
            listView.BeginUpdate();
            listView.Items.Clear();

            global::Helper.EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs("Try to open " + newItemContainer.Path));

            //create a list of items and add them to current ListView

            // GetItems ones. Each GetItems trigger filesystem get files and directory.
            List<Entity> entities = newItemContainer.GetEntities().ToList();

            var dirs = entities.Where(x => x.IsDirectory);
            var files = entities.Where(x => x.IsEntity).ToList();

            files.Sort((a, b) => String.Compare(a.Attributes.Keys.ToArray()[sortIndex], b.Attributes.Keys.ToArray()[sortIndex], StringComparison.Ordinal));
            //var files = newItemContainerItems.Where(x=>x.IsFile).OrderBy(x => x.Attributes[0], StringComparer.CurrentCultureIgnoreCase).ToList();

            entities = new List<Entity>();
            entities.AddRange(dirs);
            entities.AddRange(files);

            //no item to show
            //TODO: Maybe no needed. Ensure always at least one item.
            if (entities.Count == 0)
                return;

            foreach (Entity entity in entities)
            {
                var listViewItem = new ListViewItem(entity.Name, 0)
                {
                    Tag = entity,
                    Text = entity.Name,
                    Name = entity.Path,
                    ImageIndex = entity.IsDirectory ? 0 : 1 // Name is used as key in ListViewItem, we are using Path on each item since it's unique
                };
                // set correct image based on folder and file

                //Add extra column info such as dir, size attr
                foreach (string key in entity.Attributes.Keys)
                {
                    listViewItem.SubItems.Add(entity.Attributes[key]);
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
            listView.EndUpdate();
            listView.SuspendLayout();

            int countDir = entities.Count(x => x.IsDirectory && !x.IsParent);
            int countFiles = entities.Count(x => x.IsEntity);
            global::Helper.EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs(countFiles + " files and " + countDir + " directories listed."));
        }

        private void StartCopyOperation()
        {
            _copyDialogSubscription = global::Helper.EventAggregator.EventAggregator.Instance.Subscribe<global::SM.EventTypes.CopyDialogEventArgs>(this.HandleCopyDialogResults);

            FormComfirmCopy formCopy = new FormComfirmCopy();
            formCopy.ShowDialog();
        }
        private async void HandleCopyDialogResults(global::SM.EventTypes.CopyDialogEventArgs copyEventAgrs)
        {
            if (copyEventAgrs.ConfirmationDialog == global::SM.EventTypes.ConfirmationDialog.OK)
            {
                FormProgress formProgress = new FormProgress();
                formProgress.Show();

                IEnumerable<string> selectedDirectories = currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as Entity).Path);

                CancellationToken cancellationToken = new CancellationToken(false);
                await GetDirectoryAggregatorInfoAsync(selectedDirectories, cancellationToken).ConfigureAwait(true);

                global::Helper.EventAggregator.EventAggregator.Instance.UnSubscribe(_copyDialogSubscription);

                global::SM.EventTypes.CopyProgressEventArgs copyProgressEventArgs = new global::SM.EventTypes.CopyProgressEventArgs();
                copyProgressEventArgs.OperationStatus = global::SM.EventTypes.OperationStatus.Initiated;
                global::Helper.EventAggregator.EventAggregator.Instance.Publish(copyProgressEventArgs);

                string[] directories = currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as Entity).Path).ToArray();
                _logger.Info("Copy: " + "Operation started.");
                await Task.Run(() => HelperIO.DirectoryCopy(directories, @"R:\", true, true)).ConfigureAwait(true);
                _logger.Info("Copy: " + "Operation ended successfully.");
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
        private static async Task GetDirectoryAggregatorInfoAsync(IEnumerable<string> items, CancellationToken cancellationToken)
        {
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
                            global::Helper.EventAggregator.EventAggregator.Instance.Publish<global::SM.EventTypes.FileCountArgs>(new global::SM.EventTypes.FileCountArgs(countFiles, countSize));
                        }
                    }
                    global::Helper.EventAggregator.EventAggregator.Instance.Publish<global::SM.EventTypes.FileCountArgs>(new global::SM.EventTypes.FileCountArgs(countFiles, countSize));
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                _logger.Error(ex.Message);
                //EventAggregator.EventAggregator.Instance.Publish<Event.LogMessageArgs>(new Event.LogMessageArgs(ex.Message));
            }
        }

        private void buttonService_Click(object sender, EventArgs e)
        {
            var itemContainer = new ServiceContainer(@"");

            var entities = itemContainer.GetEntities();
            var entity = entities.FirstOrDefault();
            UpdateColumnHeaders(currentListView, entity?.Attributes.Keys.ToArray());
            UpdateListBox(leftListView, itemContainer);
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            var itemContainer = new ProcessContainer(@"");

            var entities = itemContainer.GetEntities();
            var entity = entities.FirstOrDefault();

            UpdateColumnHeaders(currentListView, entity?.Attributes.Keys.ToArray());
            UpdateListBox(leftListView, itemContainer);
        }

        private void UpdateColumnHeaders(ListView listView, string[] columnNames)
        {
            listView.SuspendLayout();

            // Remove all column except the first (Name) column.
            int columns = listView.Columns.Count;
            for (int i = 1; i < columns; i++)
            {
                listView.Columns.RemoveAt(1);
            }

            // Add new columns
            foreach (string columnName in columnNames)
            {
                var columnHeader = new ColumnHeader();
                columnHeader.AutoResize(ColumnHeaderAutoResizeStyle.None);
                columnHeader.Text = columnName;
                listView.Columns.Add(columnHeader);
            }

            listView.ResumeLayout();
        }

        private void buttonMedia_Click(object sender, EventArgs e)
        {
            Entity entity0 = currentListView.Tag as Entity;

            var itemContainer = new MediaMetaTag(entity0.Path) ;

            var entities = itemContainer.GetEntities();
            var entity = entities.FirstOrDefault();

            UpdateColumnHeaders(currentListView, entity?.Attributes.Keys.ToArray());
            UpdateListBox(leftListView, itemContainer);
        }
    }
}