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
            Headers.Add(new Header("Name", typeof(string), 55));
            Headers.Add(new Header("Ext", typeof(string), 10));
            Headers.Add(new Header("Size", typeof(long), 10));
            Headers.Add(new Header("Date", typeof(DateTime), 15));
            Headers.Add(new Header("Attr", typeof(string), 10));
            Entity = new FileSystemEntity(path);
        }
    }
}