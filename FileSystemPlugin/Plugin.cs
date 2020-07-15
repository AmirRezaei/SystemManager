using System;
using PluginInterface;

namespace FileSystemPlugin
{
    public class Plugin : PluginInterface.Plugin
    {
        public Plugin(string path)
        {
            base.Name = "File system Plugin";
            base.Author = "Amir Rezaei";
            base.Description = "File system plugin that support move, copy, delete operations";
            base.Version = "1.0";
            base.Entity = new FileSystemEntity(path);
        }
    }
}