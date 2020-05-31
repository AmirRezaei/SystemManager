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
                //itemContainer.AddRange(Directory.GetDirectories(Path).Select(x => new FileSystemContainer(new DirectoryInfo(x).Name, x)).ToArray());
                foreach (var directory in Directory.GetDirectories(Path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    FileSystemContainer fileSystemItemContainer = new FileSystemContainer(directoryInfo.Name, directoryInfo.FullName);
                    fileSystemItemContainer.Attributes.Add("");
                    fileSystemItemContainer.Attributes.Add("<DIR>");
                    fileSystemItemContainer.Attributes.Add(directoryInfo.CreationTime.ToString());

                    string attribute = "";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";

                    fileSystemItemContainer.Attributes.Add(attribute);
                    itemContainer.Add(fileSystemItemContainer);
                }
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
                //items.AddRange(Directory.GetDirectories(Path).Select(x => new FileSystemItem(new DirectoryInfo(x).Name, x)).ToArray());
                foreach (string file in Directory.GetFiles(Path))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FileSystemItem fileSystemItem = new FileSystemItem(fileInfo.Name, fileInfo.FullName);
                    fileSystemItem.Attributes.Add(fileInfo.Extension.ToString());
                    fileSystemItem.Attributes.Add(fileInfo.Length.ToString());
                    fileSystemItem.Attributes.Add(fileInfo.CreationTime.ToString());

                    string attribute = "";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemItem.Attributes.Add(attribute);

                    items.Add(fileSystemItem);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return items;
        }
    }
}
