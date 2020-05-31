using System;
using System.Collections.Generic;

namespace SystemManager
{
    /// <summary>
    /// Directory
    /// </summary>
    public abstract class ItemContainer
    {
        public bool IsDirectory { get; set; }
        public List<string> Attributes = new List<string>();
        public bool IsRoot => Parent.Equals(this);
        public abstract ItemContainer Parent { get; }
        public ItemContainer(string path)
        {
            Path = path;
        }
        public string Name { get; set; }
        public string Path { get; set; }

        private IEnumerable<ItemContainer> ItemContainers = new List<ItemContainer>();
        public abstract IEnumerable<ItemContainer> GetItems();

        public override string ToString()
        {
            return Name;
        }
    }
}
