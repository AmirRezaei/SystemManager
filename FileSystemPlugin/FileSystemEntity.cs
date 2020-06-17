using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Helper;
using PluginInterface;

namespace FileSystemPlugin
{
    public class FileSystemEntity : Entity
    {
        public FileSystemEntity(string path) : base(path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            Name = "[" + directoryInfo.Name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!
        }

        public override IEnumerable<Entity> Seek(string path)
        {
            Previous = new FileSystemEntity(Path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (directoryInfo.Parent == null)
            {
                IsDirectory = true;
                IsRoot = true;
                Parent = Path;
                Path = path;
            }
            else
            {
                IsDirectory = true;
                IsRoot = false;
                Parent = directoryInfo.Parent.FullName;
                Path = directoryInfo.FullName;
            }
            Name = "[" + directoryInfo.Name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!
            Entities = GetEntities();
            return Entities;
        }

        public override IEnumerable<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();

            try
            {
                if (!IsRoot)
                    entities.Add(new FileSystemEntity(Parent) { Name = "[..]", IsDirectory = true, IsParent = true });

                var directoryInfo = new DirectoryInfo(Path);
                foreach (DirectoryInfo dirInfo in directoryInfo.EnumerateDirectories())
                {
                    FileSystemEntity fileSystemContainer = new FileSystemEntity(dirInfo.FullName) { IsDirectory = true };
                    fileSystemContainer.Attributes.Add("Ext", "");
                    fileSystemContainer.Attributes.Add("Size", "");
                    fileSystemContainer.Attributes.Add("Date", dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    string attribute = "";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += dirInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add("Attr", attribute);

                    entities.Add(fileSystemContainer);
                }

                foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
                {
                    FileSystemEntity fileSystemContainer = new FileSystemEntity(fileInfo.FullName) { IsDirectory = false };
                    fileSystemContainer.Name = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
                    fileSystemContainer.Attributes.Add("Ex", fileInfo.Extension.TrimStart('.'));
                    fileSystemContainer.Attributes.Add("Size", fileInfo.Length.ToHumanReadable());
                    fileSystemContainer.Attributes.Add("Date", fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    string attribute = "";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attribute += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemContainer.Attributes.Add("Attr", attribute);

                    entities.Add(fileSystemContainer);
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
