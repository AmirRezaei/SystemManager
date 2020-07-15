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
        private enum TagNames
        { 
            Name,
            Ext,
            Size,
            Date,
            Attr
        }
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
            Tags.Add(new Tag(nameof(TagNames.Name), typeof(string), 55));
            Tags.Add(new Tag(nameof(TagNames.Ext), typeof(string), 10));
            Tags.Add(new Tag(nameof(TagNames.Size), typeof(long), 10));
            Tags.Add(new Tag(nameof(TagNames.Date), typeof(DateTime), 15));
            Tags.Add(new Tag(nameof(TagNames.Attr),typeof(string), 10));

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
                    FileSystemEntity fileSystemEntity = new FileSystemEntity(dirInfo.FullName) { IsDirectory = true };
                    fileSystemEntity.Values.Add(nameof(TagNames.Ext), "");
                    fileSystemEntity.Values.Add(nameof(TagNames.Size), "");
                    fileSystemEntity.Values.Add(nameof(TagNames.Date), dirInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    string attributes = "";
                    attributes += dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attributes += dirInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attributes += dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attributes += dirInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemEntity.Values.Add("Attr", attributes);

                    entities.Add(fileSystemEntity);
                }

                foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
                {
                    FileSystemEntity fileSystemEntity = new FileSystemEntity(fileInfo.FullName) { IsDirectory = false };
                    fileSystemEntity.Name = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
                    fileSystemEntity.Values.Add(nameof(TagNames.Ext), fileInfo.Extension.TrimStart('.'));
                    fileSystemEntity.Values.Add(nameof(TagNames.Size), fileInfo.Length.ToHumanReadable());
                    fileSystemEntity.Values.Add(nameof(TagNames.Date), fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    string attributes = "";
                    attributes += fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "-";
                    attributes += fileInfo.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "-";
                    attributes += fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "-";
                    attributes += fileInfo.Attributes.HasFlag(FileAttributes.System) ? "S" : "-";
                    fileSystemEntity.Values.Add("Attr", attributes);

                    entities.Add(fileSystemEntity);
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
