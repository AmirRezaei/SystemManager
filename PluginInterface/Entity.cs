using System.Collections.Generic;

namespace PluginInterface
{
    /// <summary>
    /// Directory
    /// </summary>
    public abstract class Entity
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public bool IsDirectory { get; set; }
        public bool IsEntity => !IsDirectory;
        public bool IsParent { get; set; }
        //public List<string> Attributes = new List<string>();
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public bool IsRoot => Parent.Equals(this);
        public abstract Entity Parent { get; }

        protected Entity(string path)
        {
            Path = path;
        }
        public string Name { get; set; }
        public string Path { get; set; }
        //public abstract IEnumerable<Entity> Entities { get; set; }
        public abstract IEnumerable<Entity> GetEntities();
        public override string ToString()
        {
            return Name;
        }
    }
}
