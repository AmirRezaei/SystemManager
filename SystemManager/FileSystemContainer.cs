using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace SystemManager
{
    public class FileSystemContainer : ItemContainer
    {
        public override ItemContainer Parent { get; }

        public FileSystemContainer(string directory) : base(directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            Name = "[" + directoryInfo.Name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!

            if (directoryInfo.Parent == null)
            {
                Parent = this;
            }
            else
            {
                DirectoryInfo parent = new DirectoryInfo(directory).Parent;
                Parent = new FileSystemContainer(parent.FullName);
            }
        }

        public override IEnumerable<ItemContainer> GetItems()
        {
            List<ItemContainer> listItemContainer = new List<ItemContainer>();

            if (!IsRoot)
                listItemContainer.Add(new FileSystemContainer(Parent.Path) { Name = "[..]", IsDirectory = true });

            try
            {
                //itemContainer.AddRange(Directory.GetDirectories(Path).Select(x => new FileSystemContainer(new DirectoryInfo(x).Name, x)).ToArray());
                foreach (var directory in Directory.GetDirectories(Path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    FileSystemContainer fileSystemItemContainer = new FileSystemContainer(directoryInfo.FullName);
                    fileSystemItemContainer.Name = "[" + directoryInfo.Name + "]";
                    fileSystemItemContainer.IsDirectory = true;
                    fileSystemItemContainer.Attributes.Add("");
                    fileSystemItemContainer.Attributes.Add("<DIR>");
                    fileSystemItemContainer.Attributes.Add(directoryInfo.CreationTime.ToString());

                    string attribute = "";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";

                    fileSystemItemContainer.Attributes.Add(attribute);
                    listItemContainer.Add(fileSystemItemContainer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                foreach (string file in Directory.GetFiles(Path))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FileSystemContainer fileSystemItem = new FileSystemContainer(fileInfo.FullName);
                    fileSystemItem.Name = fileInfo.Name;
                    fileSystemItem.IsDirectory = false;
                    fileSystemItem.Attributes.Add(fileInfo.Extension.TrimStart('.'));
                    fileSystemItem.Attributes.Add(fileInfo.Length.ToHumanReadable());
                    fileSystemItem.Attributes.Add(fileInfo.CreationTime.ToString());

                    string attribute = "";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemItem.Attributes.Add(attribute);

                    listItemContainer.Add(fileSystemItem);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return listItemContainer;
        }
    }
}
