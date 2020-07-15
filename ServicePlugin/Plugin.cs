using System;
using PluginInterface;

namespace ServicePlugin
{
    public class Plugin : PluginInterface.Plugin
    {
        public Plugin(string path)
        {
            base.Name = "Service Plugin";
            base.Author = "Amir Rezaei";
            base.Description = "Show all services.";
            base.Version = "1.0";
            Entity = new ServiceEntity(path);
        }
    }
}