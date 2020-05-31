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

namespace SystemManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            leftlistView.MouseDown += new MouseEventHandler(ListView_MouseDown);
            leftlistView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);
            leftlistView.Resize += new EventHandler(ListView_Resize);
            leftlistView.KeyDown += new KeyEventHandler(ListView_KeyDown);
            leftlistView.KeyPress += new KeyPressEventHandler(LeftlistView_KeyPress);
            leftlistView.ItemActivate += new EventHandler(ListView_ItemActivate);
        }

        private void ListView_ItemActivate(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                Button buttom = new Button
                {
                    Text = drive,
                    Size = new System.Drawing.Size(75, 35),
                    Anchor = AnchorStyles.Left
                };
                buttom.Click += LeftVolumeButtom_Click;
                panelDriveLeft.Controls.Add(buttom);
            }
            panelDriveLeft.Refresh();
        }

        private void LeftlistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        ItemContainer itemContainer = leftlistView.FocusedItem.Tag as ItemContainer;
                        UpdateListBox(itemContainer);
                        break;
                    }
                case Keys.Back:
                    {
                        ItemContainer itemContainer = leftlistView.Tag as ItemContainer;

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
            ListViewHitTestInfo info = leftlistView.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                ItemContainer itemContainer = item.Tag as ItemContainer;
                UpdateListBox(itemContainer);
            }
            else
            {
                leftlistView.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            //ListViewHitTestInfo info = leftlistView.HitTest(e.X, e.Y);
            //ListViewItem item = info.Item;

            //if (item != null)
            //{
            //    this.textBox1.Text = item.Text;
            //}
            //else
            //{
            //    this.listView1.SelectedItems.Clear();
            //    this.textBox1.Text = "No Item is Selected";
            //}
        }

        private void LeftVolumeButtom_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            string volumePath = button.Text;

            var itemContainter = new FileSystemContainer(volumePath, volumePath);
            leftlistView.Tag = itemContainter;
            UpdateListBox(itemContainter);
        }

        private void UpdateListBox(ItemContainer newItemContainer)
        {
            ItemContainer oldItemContainer = leftlistView.Tag as ItemContainer;
            leftlistView.Tag = newItemContainer;

            leftlistView.Items.Clear();

            foreach (ItemContainer itemContainer in newItemContainer.GetItemContainers())
            {
                ListViewItem listViewItem = new ListViewItem(itemContainer.Name, 0)
                {
                    Tag = itemContainer,
                    Name = itemContainer.Name
                };
                leftlistView.Items.Add(listViewItem);
            }
            foreach (Item item in newItemContainer.GetItems())
            {
                ListViewItem listViewItem = new ListViewItem(item.Name, 1)
                {
                    Tag = item,
                    Name = item.Name
                };

                leftlistView.Items.Add(listViewItem);
            }

            //Focus on right ListViewItem
            ListViewItem focusListViewItem = leftlistView.Items.Cast<ListViewItem>().Where(x => x.Text == oldItemContainer.Name).FirstOrDefault();
            if (focusListViewItem == null)
            {
                leftlistView.Items[0].Selected = true;
                leftlistView.Items[0].Focused = true;
            }
            else
            {
                // When we navigate back, we want to focus on right ListViewItem
                leftlistView.Items[oldItemContainer.Name].Selected = true;
                leftlistView.Items[oldItemContainer.Name].Focused = true;
            }
        }
        private void ListView_Resize(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            int x = listview.Width / 100;
            listview.Columns[0].Width = x * 80;
            listview.Columns[1].Width = x * 5;
            listview.Columns[2].Width = x * 5;
            listview.Columns[3].Width = x * 5;
        }
    }
}
