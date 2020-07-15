using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginBase
{
    //The plugin host stores all plugin instances by name and allows unloading them. We load the assembly into the _pluginAssemblyLoadingContext. After that, the Activator creates a new instance of our plugin types and adds it to the dictionary.

    /// <summary>
    /// The plugin finder is responsible for loading and scanning an assembly for plugins. This means we need to store the information about which assemblies have plugins and unload the assembly after scanning.
    /// </summary>
    /// <typeparam name="TPlugin"></typeparam>
    public class PluginFinder<TPlugin> where TPlugin : IPlugin
    {
        public PluginFinder() { }

        public IReadOnlyCollection<string> FindAssemliesWithPlugins(string path)
        {
            var assemblies = Directory.GetFiles(path, "*.dll", new EnumerationOptions() { RecurseSubdirectories = true });
            return FindPluginsInAssemblies(assemblies);
        }

        private IReadOnlyCollection<string> FindPluginsInAssemblies(string[] assemblyPaths)
        {
            var assemblyPluginInfos = new List<string>();
            var pluginFinderAssemblyContext = new PluginAssemblyLoadingContext(name: "PluginFinderAssemblyContext");
            foreach (var assemblyPath in assemblyPaths)
            {
                var assembly = pluginFinderAssemblyContext.LoadFromAssemblyPath(assemblyPath);
                if (GetPluginTypes(assembly).Any())
                {
                    assemblyPluginInfos.Add(assembly.Location);
                }
            }
            pluginFinderAssemblyContext.Unload();
            return assemblyPluginInfos;
        }

        public static IReadOnlyCollection<Type> GetPluginTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    typeof(TPlugin).IsAssignableFrom(type))
                .ToArray();
        }
    }
}
