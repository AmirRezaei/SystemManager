using System;
using System.Collections.Generic;
using PluginInterface;

namespace PluginBase
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }

        int Execute();
    }

    public class PluginBase
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }

        public List<Tag> Headers = new List<Tag>();
        public Entity Entity { get; set; }
    }
}