using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemManager.EventAggregator;
using SystemManager.Event;

namespace SystemManager
{
    public class FileSystemContainer : ItemContainer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public override ItemContainer Parent { get; }
        public FileSystemContainer(string directory) : base(directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            Name = "[" + directoryInfo.Name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!

            Parent = directoryInfo.Parent == null ? this : new FileSystemContainer(directoryInfo.Parent.FullName);
        }

        public override IEnumerable<ItemContainer> GetItems()
        {
            List<ItemContainer> listItemContainer = new List<ItemContainer>();

            try
            {
                if (!IsRoot)
                    //yield return (new FileSystemContainer(Parent.Path) { Name = "[..]", IsDirectory = true, IsParent = true });
                    listItemContainer.Add(new FileSystemContainer(Parent.Path) { Name = "[..]", IsDirectory = true, IsParent = true });

                //try
                //{
                string attribute = "";
                var directoryInfo = new DirectoryInfo(Path);
                foreach (DirectoryInfo dirInfo in directoryInfo.EnumerateDirectories())
                {
                    FileSystemContainer fileSystemContainer = new FileSystemContainer(dirInfo.FullName)
                    {
                        Name = "[" + dirInfo.Name + "]", IsDirectory = true
                    };
                    fileSystemContainer.Attributes.Add("");
                    fileSystemContainer.Attributes.Add("<DIR>");
                    fileSystemContainer.Attributes.Add(dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture));

                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add(attribute);

                    listItemContainer.Add(fileSystemContainer);
                    //yield return fileSystemItemContainer;
                }

                foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
                {
                    FileSystemContainer fileSystemContainer = new FileSystemContainer(fileInfo.FullName)
                    {
                        Name = fileInfo.Name, IsDirectory = false
                    };
                    fileSystemContainer.Attributes.Add(fileInfo.Extension.TrimStart('.'));
                    fileSystemContainer.Attributes.Add(fileInfo.Length.ToHumanReadable());
                    fileSystemContainer.Attributes.Add(fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));

                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add(attribute);

                    listItemContainer.Add(fileSystemContainer);
                    //yield return fileSystemItem;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return listItemContainer;

            //foreach (var directory in Directory.GetDirectories(Path))
            //{
            //    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            //    FileSystemContainer fileSystemItemContainer = new FileSystemContainer(directoryInfo.FullName);
            //    fileSystemItemContainer.Name = "[" + directoryInfo.Name + "]";
            //    fileSystemItemContainer.IsDirectory = true;
            //    fileSystemItemContainer.Attributes.Add("");
            //    fileSystemItemContainer.Attributes.Add("<DIR>");
            //    fileSystemItemContainer.Attributes.Add(directoryInfo.CreationTime.ToString());

            //    string attribute = "";
            //    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
            //    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
            //    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
            //    attribute += directoryInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";

            //    fileSystemItemContainer.Attributes.Add(attribute);
            //    listItemContainer.Add(fileSystemItemContainer);
            //}

            //foreach (string file in Directory.GetFiles(Path))
            //{
            //    FileInfo fileInfo = new FileInfo(file);
            //    FileSystemContainer fileSystemItem = new FileSystemContainer(fileInfo.FullName);
            //    fileSystemItem.Name = fileInfo.Name;
            //    fileSystemItem.IsDirectory = false;
            //    fileSystemItem.Attributes.Add(fileInfo.Extension.TrimStart('.'));
            //    fileSystemItem.Attributes.Add(fileInfo.Length.ToHumanReadable());
            //    fileSystemItem.Attributes.Add(fileInfo.CreationTime.ToString());

            //    string attribute = "";
            //    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
            //    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
            //    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
            //    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
            //    fileSystemItem.Attributes.Add(attribute);

            //    listItemContainer.Add(fileSystemItem);
            //}
            //}
            //catch (Exception ex)
            //{
            //    //EventAggregator.EventAggregator.Instance.Publish(new LogMessageArgs(ex.Message));
            //    Logger.Error(ex.Message);
            //}

            //return listItemContainer;
        }
    }
}
