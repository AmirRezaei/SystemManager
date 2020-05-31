namespace SystemManager
{
    /// <summary>
    /// File
    /// </summary>
    public abstract class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Item(string name, string path)
        {
            Name = "[" + name + "]";
            Path = path;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
