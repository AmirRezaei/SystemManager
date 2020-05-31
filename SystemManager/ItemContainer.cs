using System.Collections.Generic;

namespace SystemManager
{
    /// <summary>
    /// Directory
    /// </summary>
    public abstract class ItemContainer
    {
        public List<string> Attributes = new List<string>();
        public bool IsRoot => Parent.Equals(this);
        public abstract ItemContainer Parent { get; }
        public ItemContainer(string name, string path)
        {
            Name = "[" + name + "]";
            Path = path;
        }
        public string Name { get; set; }
        public string Path { get; set; }

        private IEnumerable<ItemContainer> ItemContainers = new List<ItemContainer>();
        private IEnumerable<Item> Items = new List<Item>();

        public abstract IEnumerable<ItemContainer> GetItemContainers();
        public abstract IEnumerable<Item> GetItems();

        public override string ToString()
        {
            return Name;
        }
    }
}
