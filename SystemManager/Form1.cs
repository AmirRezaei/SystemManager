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

namespace SystemManager
{
    public partial class Form1 : Form
    {
        public ListView CurrentListView { get; set; }
        public enum SenderSide
        {
            Left,
            Right
        }

        public Form1()
        {
            InitializeComponent();

            leftListView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            leftListView.Resize += new EventHandler(ListView_Resize);
            leftListView.KeyDown += new KeyEventHandler(ListView_KeyDown);
            leftListView.GotFocus += ListView_GotFocus;
            leftListView.MouseDown += new MouseEventHandler(ListView_MouseDown);

            rightListView.Resize += new EventHandler(ListView_Resize);
            rightListView.KeyDown += new KeyEventHandler(ListView_KeyDown);
            rightListView.GotFocus += ListView_GotFocus;
            rightListView.MouseDown += new MouseEventHandler(ListView_MouseDown);
            //rightListView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            //leftListView.KeyPress += new KeyPressEventHandler(LeftlistView_KeyPress);
            //leftListView.ItemActivate += new EventHandler(ListView_ItemActivate);
        }

        private void ListView_GotFocus(object sender, EventArgs e)
        {
            CurrentListView = sender as ListView;
            System.Diagnostics.Debug.WriteLine(CurrentListView.Name);
        }

        private void ListView_ItemActivate(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void LeftlistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
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

                    //buttom.Tag = i == 0 ? SenderSide.Left : SenderSide.Right;
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
            CurrentListView = rightListView;
            rightListView.Tag = fileSystemContainter;
            UpdateListBox(fileSystemContainter);

            CurrentListView = leftListView;
            leftListView.Tag = fileSystemContainter;
            UpdateListBox(fileSystemContainter);
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        ItemContainer itemContainer = listView.FocusedItem.Tag as ItemContainer;
                        if (itemContainer.IsDirectory)
                            UpdateListBox(itemContainer);
                        break;
                    }
                case Keys.Back:
                    {
                        ItemContainer itemContainer = listView.Tag as ItemContainer;

                        //No need to update if we already are in root folder.
                        if (!itemContainer.IsRoot)
                        {
                            UpdateListBox(itemContainer.Parent);
                        }

                        break;
                    }
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;

            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                ItemContainer itemContainer = item.Tag as ItemContainer;
                UpdateListBox(itemContainer);
            }
            else
            {
                listView.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Debug.Write(e.Button);
                ListViewHitTestInfo info = CurrentListView.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;

                if (item != null)
                {
                    item.Checked = !item.Checked;
                    item.Selected = item.Checked;
                }
            }
        }

        private void VolumeButtom_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            string volumePath = button.Text;
            var fileSystemContainter = new FileSystemContainer(volumePath);

            string senderSide = button.Tag as string;
            if (senderSide == SenderSide.Left.ToString())
            {
                CurrentListView = leftListView;
                leftListView.Tag = fileSystemContainter;
            }
            else
            {
                CurrentListView = rightListView;
                rightListView.Tag = fileSystemContainter;
            }
            UpdateListBox(fileSystemContainter);
        }

        private void UpdateListBox(ItemContainer newItemContainer)
        {
            //if (newItemContainer.Name == null)
            //    Debugger.Break();

            ItemContainer oldItemContainer = CurrentListView.Tag as ItemContainer;
            CurrentListView.Tag = newItemContainer;

            CurrentListView.Items.Clear();

            //create a list of items and add them to current ListView
            foreach (ItemContainer itemContainer in newItemContainer.GetItems())
            {
                ListViewItem listViewItem = new ListViewItem(itemContainer.Name, 0)
                {
                    Tag = itemContainer,
                    Text = itemContainer.Name,
                    Name = itemContainer.Path  // Name is used as key in ListViewItem, we are using Path on each item since it's unique
                };
                listViewItem.ImageIndex = itemContainer.IsDirectory ? 0 : 1; // set correct image based on folder and file

                //Add extra column info such as dir, size attr
                foreach (var attribute in itemContainer.Attributes)
                {
                    listViewItem.SubItems.Add(attribute);
                }

                CurrentListView.Items.Add(listViewItem);
            }


            //Focus on right ListViewItem
            ListViewItem focusListViewItem = CurrentListView.Items.Cast<ListViewItem>().Where(x => x.Text == oldItemContainer.Name).FirstOrDefault();
            if (focusListViewItem == null)
            {
                CurrentListView.Items[0].Selected = true;
                CurrentListView.Items[0].Focused = true;
            }
            else
            {
                // When we navigate back, we want to focus on right ListViewItem
                CurrentListView.Items[oldItemContainer.Path].Selected = true;
                CurrentListView.Items[oldItemContainer.Path].Focused = true;
            }
        }
        private void ListView_Resize(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            int x = listview.Width / 100;
            listview.Columns[0].Width = x * 70;
            listview.Columns[1].Width = x * 5;
            listview.Columns[2].Width = x * 10;
            listview.Columns[3].Width = x * 10;
            listview.Columns[4].Width = x * 5;
        }
    }
}
