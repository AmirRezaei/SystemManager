using System;
using System.Collections.Generic;

namespace PluginBase
{
    public class PluginHost<TPlugin> where TPlugin : IPlugin
    {
        private Dictionary<string, TPlugin> _plugins = new Dictionary<string, TPlugin>();
        private readonly PluginAssemblyLoadingContext _pluginAssemblyLoadingContext;

        public PluginHost()
        {
            _pluginAssemblyLoadingContext = new PluginAssemblyLoadingContext("PluginAssemblyContext");
        }

        public TPlugin GetPlugin(string pluginName)
        {
            return _plugins[pluginName];
        }

        public IReadOnlyCollection<TPlugin> GetPlugins()
        {
            return _plugins.Values;
        }

        public void LoadPlugins(IReadOnlyCollection<string> assembliesWithPlugins)
        {
            foreach (var assemblyPath in assembliesWithPlugins)
            {
                var assembly = _pluginAssemblyLoadingContext.LoadFromAssemblyPath(assemblyPath);
                var validPluginTypes = PluginFinder<TPlugin>.GetPluginTypes(assembly);
                foreach (var pluginType in validPluginTypes)
                {
                    var plutinInstance = (TPlugin)Activator.CreateInstance(pluginType);
                    RegisterPlugin(plutinInstance);
                }
            }
        }

        public void Unload()
        {
            _plugins.Clear();
            _pluginAssemblyLoadingContext.Unload();
        }
    }
}