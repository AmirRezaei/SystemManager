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
using MediaMetaData;
using SM.Controls;
using SM.EventTypes;
using Plugin = FileSystemPlugin.Plugin;

namespace SM
{
    public partial class SystemManager : Form
    {
        private Subscription<global::SM.EventTypes.CopyDialogEventArgs> _copyDialogSubscription;
        private static NLog.Logger _logger;

        private SenderSide senderSide;
        private ListViewEx _currentListView;
        private TextBox _currentStatusTextBox;
        public enum SenderSide
        {
            Left,
            Right
        }

        public SystemManager()
        {
            InitializeComponent();
            leftListView.MouseDoubleClick += ListView_MouseDoubleClick;
            leftListView.GotFocus += ListView_GotFocus;
            leftListView.KeyDown += ListView_KeyDown;

            rightListView.MouseDoubleClick += ListView_MouseDoubleClick;
            rightListView.KeyDown += ListView_KeyDown;
            rightListView.GotFocus += ListView_GotFocus;

            WindowState = FormWindowState.Maximized;
            Text = Application.ProductName + @" " + Application.ProductVersion;

            textBoxFilter.TextChanged += TextBoxFilter_TextChanged;
            textBoxFilter.KeyDown += (o, args) =>
            {
                if (args.KeyCode == Keys.Escape)
                {
                    textBoxFilter.Hide();
                    textBoxFilter.Clear(); // clear after Hide(), otherwise TextChange event will trigger.
                    _currentListView.Focus();
                    args.Handled = true;
                }
            };
        }

        private void TextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Plugin plugin = _currentListView.Tag as Plugin;

            if(!textBox.Visible)
                return;

            // Call FindItemWithText with the contents of the textbox.
            ListViewItem foundItem = _currentListView.FindItemWithText(textBox.Text, false, 1, true);
            if (foundItem != null)
            {
                foreach (ListViewItem listViewItem in _currentListView.Items)
                {
                    if (listViewItem == foundItem)
                    {
                        listViewItem.Selected = true;
                        listViewItem.Focused = true;
                        listViewItem.EnsureVisible();
                    }
                    else
                    {
                        listViewItem.Selected = false;
                    }
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;

            try
            {
                if (e.Alt)
                {
                    // Filter textBox
                    textBoxFilter.Show();
                    textBoxFilter.Focus();
                    e.Handled = true;
                }

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            PluginInterface.Plugin plugin = listView.Tag as PluginInterface.Plugin;
                            var entity = listView.FocusedItem.Tag as Entity;
                            if (entity.IsDirectory)
                            {
                                plugin.Entity.Seek(entity.Path);
                                UpdateListBox(listView, plugin);
                            }
                            break;
                        }
                    case Keys.Back:
                        {
                            PluginInterface.Plugin plugin = listView.Tag as PluginInterface.Plugin;
                            //No need to update if we already are in root folder.
                            if (!plugin.Entity.IsRoot)
                            {
                                plugin.Entity.Seek(plugin.Entity.Parent);
                                UpdateListBox(listView, plugin);
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

                //e.Handled = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;

            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                PluginInterface.Plugin plugin = listView.Tag as PluginInterface.Plugin;
                Entity entity = item.Tag as Entity;
                if (entity.IsDirectory)
                    plugin.Entity.Seek(entity.Path);
                UpdateListBox(listView, plugin);
            }
            else
            {
                listView.SelectedItems.Clear();
                //EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs("No Item is selected."));
            }
        }

        private void ListView_GotFocus(object sender, EventArgs e)
        {
            _currentListView = sender as ListViewEx;

            if (_currentListView.Name.Contains("left"))
            {
                _currentStatusTextBox = textBoxStatus1;
            }
            else
            {
                _currentStatusTextBox = textBoxStatus2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Init logger after form has been loaded and to ensure logRichTextBox is initiated.
            _logger = NLog.LogManager.GetCurrentClassLogger();

            // Force resize splitContainer1 to 50%
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = false;
            splitContainer1.SplitterDistance = (int)(splitContainer1.ClientSize.Width * 0.50);
            CreateDriveButtons();

            //TODO fix active listView
            _currentListView = leftListView;
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

            var fileSystemPlugin = new Plugin(@"C:\");
            //TODO: interim solution just init both listViews
            fileSystemPlugin.Entity.Seek(@"C:\");

            rightListView.Tag = fileSystemPlugin;
            rightListView.UpdateColumnHeaders(fileSystemPlugin.Headers);
            UpdateListBox(rightListView, fileSystemPlugin);

            leftListView.Tag = fileSystemPlugin;
            leftListView.UpdateColumnHeaders(fileSystemPlugin.Headers);
            UpdateListBox(leftListView, fileSystemPlugin);

            leftListView.Focus();
        }

        private void VolumeButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            string volumePath = button.Text;
            var fileSystemPlugin = new FileSystemPlugin.Plugin(volumePath);
            fileSystemPlugin.Entity.Seek(volumePath);

            string senderSide = button.Tag as string;
            if (senderSide == SystemManager.SenderSide.Left.ToString())
            {
                UpdateListBox(leftListView, fileSystemPlugin);
            }
            else
            {
                UpdateListBox(rightListView, fileSystemPlugin);
            }
        }

        private void UpdateListBox(ListView listView, PluginInterface.Plugin plugin, string filter = "")
        {
            //Plugin oldPlugin = listView.Tag as Plugin;
            listView.Tag = plugin;

            listView.SuspendLayout();
            listView.BeginUpdate();
            listView.Items.Clear();

            // GetItems ones. Each GetItems trigger filesystem get files and directory.
            Entity parent = plugin.Entity.Entities.FirstOrDefault(x => x != null && x.IsParent);
            IEnumerable<Entity> dirs = plugin.Entity.Entities.Where(x => x != null && !x.IsParent && x.IsDirectory);
            List<Entity> files = plugin.Entity.Entities.Where(x => x != null && x.IsFile).ToList();
            //files.Sort((a, b) => String.Compare(a.Attributes.Keys.ToArray()[sortIndex], b.Attributes.Keys.ToArray()[sortIndex], StringComparison.Ordinal));

            var entities = new List<Entity>();
            entities.AddRange(dirs);
            entities.AddRange(files);

            //no item to show
            //TODO: Maybe no needed. Ensure always at least one item.
            if (entities.Count == 0)
                return;

            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (Entity entity in entities.Where(x => !x.IsParent && x.Name.StartsWith(filter)))
            {
                //var listViewItem = new ListViewItem(entity.Name, 0)
                //{
                //    Tag = entity,
                //    Text = entity.Name, // Name is used as key in ListViewItem, we are using Path on each item since it's unique
                //    Name = entity.Path,
                //    ImageIndex = entity.IsDirectory ? 0 : 1 // set correct image based on folder and file
                //};
                ListViewItem listViewItem = new ListViewItem().FromEntity(entity);
                //Add extra column values such as dir, size attr
                listViewItem.SubItems.AddRange(entity.Attributes.Values.ToArray());
                listViewItems.Add(listViewItem);
            }

            listView.Items.AddRange(listViewItems.ToArray()); // adding listViewItems as array is much faster. Individual adding triggers ListView sorting.

            //Focus on right ListViewItem
            ListViewItem focusListViewItem = listView.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == plugin.Entity.Previous?.Name);
            if (focusListViewItem == null)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
            }
            else
            {
                // When we navigate back, we want to focus on right ListViewItem
                listView.Items[plugin.Entity.Previous.Path].Selected = true;
                listView.Items[plugin.Entity.Previous.Path].Focused = true;
                listView.EnsureVisible(listView.Items[plugin.Entity.Previous.Path].Index);
            }

            listView.EndUpdate();
            listView.SuspendLayout();

            int countDir = entities.Count(x => x.IsDirectory && !x.IsParent);
            int countFiles = entities.Count(x => x.IsFile);
            //            global::Helper.EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs(countFiles + " files and " + countDir + " directories listed."));
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

                IEnumerable<string> selectedDirectories = _currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as Entity).Path);

                CancellationToken cancellationToken = new CancellationToken(false);
                await GetDirectoryAggregatorInfoAsync(selectedDirectories, cancellationToken).ConfigureAwait(true);

                global::Helper.EventAggregator.EventAggregator.Instance.UnSubscribe(_copyDialogSubscription);

                global::SM.EventTypes.CopyProgressEventArgs copyProgressEventArgs = new global::SM.EventTypes.CopyProgressEventArgs();
                copyProgressEventArgs.OperationStatus = global::SM.EventTypes.OperationStatus.Initiated;
                global::Helper.EventAggregator.EventAggregator.Instance.Publish(copyProgressEventArgs);

                string[] directories = _currentListView.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => (x.Tag as Entity).Path).ToArray();
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
            //var servicePlugin = new ServicePlugin();

            //var entities = rootEntity.GetEntities();
            //var entity = entities.FirstOrDefault();
            //_currentListView.UpdateColumnHeaders(entity?.Attributes.Keys.ToArray());
            //UpdateListBox(_currentListView, servicePlugin);
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            var processPlugin = new ProcessPlugin.Plugin("");
            processPlugin.Entity.Seek("");

            _currentListView.UpdateColumnHeaders(processPlugin.Headers);
            UpdateListBox(_currentListView, processPlugin);
        }


        private void buttonMedia_Click(object sender, EventArgs e)
        {
            Entity currentEntity = _currentListView.Tag as Entity;
            currentEntity.Seek("");

            var rootEntity = new MediaMetaTag(currentEntity.Path);

            //var entities = rootEntity.GetEntities();
            //var entity = entities.FirstOrDefault();

            //_currentListView.UpdateColumnHeaders(entity?.Attributes.Keys.ToArray());
            //UpdateListBox(_currentListView, rootEntity);
        }
    }
}