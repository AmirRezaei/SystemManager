using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System;

namespace SystemManager
{
    public class FileSystemContainer : ItemContainer
    {
        public override ItemContainer Parent { get; }

        public FileSystemContainer(string name, string directory) : base(name, directory)
        {
            if (new DirectoryInfo(directory).Parent == null)
            {
                Parent = this;
            }
            else
            {
                DirectoryInfo parent = new DirectoryInfo(directory).Parent;
                Parent = new FileSystemContainer(parent.Name, parent.FullName);
            }
        }

        public override IEnumerable<ItemContainer> GetItemContainers()
        {
            List<ItemContainer> itemContainer = new List<ItemContainer>();

            if (!IsRoot)
                itemContainer.Add(new FileSystemContainer("..", Parent.Path));

            try
            {
                itemContainer.AddRange(Directory.GetDirectories(Path).Select(x => new FileSystemContainer(new DirectoryInfo(x).Name, x)).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return itemContainer;

        }

        public override IEnumerable<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                items.AddRange(Directory.GetDirectories(Path).Select(x => new FileSystemItem(new DirectoryInfo(x).Name, x)).ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return items;
        }
    }
}
