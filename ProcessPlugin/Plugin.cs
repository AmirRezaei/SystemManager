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
            Headers.Add(new Header("Name", typeof(string), 50));
            Headers.Add(new Header("Id", typeof(string), 10));
            Headers.Add(new Header("WorkingSet64", typeof(long), 10));
            Headers.Add(new Header("PagedSystemMemorySize64", typeof(long), 10));
            Headers.Add(new Header("PagedMemorySize64", typeof(long), 10));
            Headers.Add(new Header("Date", typeof(DateTime), 10));
            Entity = new ProcessEntity(path);
        }
    }
}