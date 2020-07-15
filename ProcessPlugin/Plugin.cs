using System;
using PluginInterface;

namespace ProcessPlugin
{
    public class Plugin : PluginInterface.Plugin
    {
        public Plugin(string path)
        {
            base.Name = "Process Plugin";
            base.Author = "Amir Rezaei";
            base.Description = "Show all current running processes.";
            base.Version = "1.0";
            Entity = new ProcessEntity(path);
        }
    }
}