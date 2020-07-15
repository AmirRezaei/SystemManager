using System;
using System.Collections.Generic;

namespace PluginInterface
{
    public class Plugin
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }

        public Entity Entity { get; set; }
    }
}