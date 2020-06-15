using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Helper;
using PluginInterface;

namespace SM
{
    public class FileSystemEntity : Entity
    {
        public override Entity Parent { get; }
        public FileSystemEntity(string directory) : base(directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            Name = "[" + directoryInfo.Name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!

            Parent = directoryInfo.Parent == null ? this : new FileSystemEntity(directoryInfo.Parent.FullName);
        }

        public IEnumerable<Entity> Entities { get; set; }

        public override IEnumerable<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();

            try
            {
                if (!IsRoot)
                    entities.Add(new FileSystemEntity(Parent.Path) { Name = "[..]", IsDirectory = true, IsParent = true });

                //try
                //{
                var directoryInfo = new DirectoryInfo(Path);
                foreach (DirectoryInfo dirInfo in directoryInfo.EnumerateDirectories())
                {
                    FileSystemEntity fileSystemContainer = new FileSystemEntity(dirInfo.FullName)
                    {
                        Name = "[" + dirInfo.Name + "]",
                        IsDirectory = true
                    };
                    fileSystemContainer.Attributes.Add("Ext", "");
                    fileSystemContainer.Attributes.Add("Size", "");
                    fileSystemContainer.Attributes.Add("Date", dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    //fileSystemContainer.Attributes.Add("");
                    //fileSystemContainer.Attributes.Add("<DIR>");
                    //fileSystemContainer.Attributes.Add(dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    string attribute = "";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add("Attr", attribute);

                    entities.Add(fileSystemContainer);
                    //yield return fileSystemItemContainer;
                }

                foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
                {
                    FileSystemEntity fileSystemContainer = new FileSystemEntity(fileInfo.FullName)
                    {
                        Name = fileInfo.Name,
                        IsDirectory = false
                    };

                    fileSystemContainer.Attributes.Add("Ex", fileInfo.Extension.TrimStart('.'));
                    fileSystemContainer.Attributes.Add("Size", fileInfo.Length.ToHumanReadable());
                    fileSystemContainer.Attributes.Add("Date", fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));

                    //fileSystemContainer.Attributes.Add(fileInfo.Extension.TrimStart('.'));
                    //fileSystemContainer.Attributes.Add(fileInfo.Length.ToHumanReadable());
                    //fileSystemContainer.Attributes.Add(fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));

                    string attribute = "";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add("Attr", attribute);

                    entities.Add(fileSystemContainer);
                    //yield return fileSystemItem;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return entities;
        }
    }
}
