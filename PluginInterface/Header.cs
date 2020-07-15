using System;
using System.ComponentModel;

namespace PluginInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class Tag
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public string Name { get; set; }
        public Type Type { get; set; }

        public int Width { get; set; }

        public Tag(string name, Type type, int width)
        {
            Name = name;
            Type = type;
            Width = width;
        }
    }
}
