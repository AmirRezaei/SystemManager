using System;
using System.Collections.Generic;
using NLog.Config;

namespace PluginInterface
{
    /// <summary>
    /// Entity abstract
    /// </summary>
    public abstract class Entity
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public bool IsDirectory { get; set; }
        public bool IsFile => !IsDirectory;

        public bool IsParent { get; set; }
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public bool IsRoot { get; set; } // => Parent.Equals(this);
        public Entity NavigatedFrom { get; set; }
        public string Parent { get; set; }
        public Entity(string path)
        {
            Path = path;
        }
        public string Name { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// Previous Entity before seek.
        /// </summary>
        public Entity Previous { get; set; }
        public IEnumerable<Entity> Entities { get; set; }
        public abstract IEnumerable<Entity> GetEntities();

        public void SetParent(string name, string path)
        {
            //Name = "[" + name + "]"; // Name must also be set. The Name is used as in ListView for locating specific ListViewItems[name as key]. This is due to ListView uses ListViewItem.Name as key!
            //IsDirectory = true;
            //IsRoot = false;
            //Parent = path;
            //Path = path;
            //Entities = GetEntities();
        }

        /// <summary>
        /// Seek new path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public abstract IEnumerable<Entity> Seek(string path);
        public override string ToString()
        {
            return Name;
        }
    }
}
