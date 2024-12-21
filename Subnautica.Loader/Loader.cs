using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Subnautica.API.Enums;
using Subnautica.API.Features;
using UnityEngine;
using UWE;

namespace Subnautica.Loader;

public class Loader
{
	private static SubnauticaPlugin DevToolPlugin { get; set; }
	private static FileSystemWatcher FileWatcher { get; set; }

	private static bool IsRunningFileWatcher { get; set; }
	public static void Run()
	{
		Loader.LoadDependencies();
		Loader.LoadPlugins();
		Loader.LoadDevTools();
		Loader.LoadFileWatcher();
	}


	private static void LoadDependencies()
	{
        try
        {
            string dependenciesPath = Paths.GetGameDependenciesPath();
            string[] files = Directory.GetFiles(dependenciesPath, "*.dll");
            Log.Info(string.Format("Loading dependencies: {0}, Total: {1}", dependenciesPath, files.Count()));
            foreach (string str in files)
            {
                string dependency = str;
                if (!Loader.SkippedDependencies.Any(dependency.Contains))
                {
                    Assembly assembly = Loader.LoadAssembly(dependency);
                    if (!(assembly == null))
                        Log.Info(string.Format("Loaded dependency: v{0}, Name: {1}", assembly.GetName().Version.ToString(3), assembly.GetName().Name));
                }
            }
            Log.Info("All dependencies are loaded!");
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("There was a problem installing dependencies: {0}", ex));
        }
    }

	public static void LoadPlugins()
	{
        string gamePluginsPath = Paths.GetGamePluginsPath();
        string[] files = Directory.GetFiles(gamePluginsPath, "*.dll");
        Log.Info(string.Format("Loading plugins: {0}, Total: {1}", gamePluginsPath, files.Count()));
        Dictionary<SubnauticaPluginPriority, List<SubnauticaPlugin>> source = new Dictionary<SubnauticaPluginPriority, List<SubnauticaPlugin>>();
        foreach (string str in files)
        {
            string filePath = str;
            if (!Loader.SkippedPlugins.Any(filePath.Contains))
            {
                Assembly assembly = Loader.LoadAssembly(filePath);
                if (!(assembly == null))
                {
                    SubnauticaPlugin plugin = Loader.GetPlugin(assembly);
                    if (plugin != null)
                    {
                        if (!source.ContainsKey(plugin.Priority))
                            source.Add(plugin.Priority, new List<SubnauticaPlugin>());
                        source[plugin.Priority].Add(plugin);
                        Log.Info(string.Format("Loaded plugin: v{0}, Name: {1}", assembly.GetName().Version.ToString(3), assembly.GetName().Name));
                    }
                }
            }
        }
        foreach (var keyValuePair in source.OrderBy(q => q.Key))
        {
            foreach (SubnauticaPlugin subnauticaPlugin in keyValuePair.Value)
            {
                try
                {
                    subnauticaPlugin.OnEnabled();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("An error occurred while activating the plugin: {0}, Exception: {1}", (object)subnauticaPlugin.Name, (object)ex));
                }
            }
        }
    }

	public static void LoadDevTools()
	{
        if (Loader.DevToolPlugin != null)
        {
            Loader.DevToolPlugin.OnDisabled();
            Loader.DevToolPlugin = null;
        }
        foreach (string file in Directory.GetFiles(Paths.GetGamePluginsPath(), "*.dll"))
        {
            if (file.Contains("Subnautica.DevTools"))
            {
                Assembly assembly = Loader.LoadAssembly(file);
                if (!(assembly == null))
                {
                    SubnauticaPlugin plugin = Loader.GetPlugin(assembly);
                    if (plugin != null)
                    {
                        Loader.DevToolPlugin = plugin;
                        break;
                    }
                }
            }
        }
        if (Loader.DevToolPlugin == null)
            return;
        try
        {
            Loader.DevToolPlugin.OnEnabled();
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("An error occurred while activating the Dev Tools Plugin: {0}, Exception: {1}", Loader.DevToolPlugin.Name, ex));
        }
    }

	public static void LoadFileWatcher()
	{
        if (Loader.FileWatcher != null)
            return;
        Loader.FileWatcher = new FileSystemWatcher();
        Loader.FileWatcher.Path = Paths.GetGamePluginsPath(null);
        Loader.FileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security;
        Loader.FileWatcher.Filter = "Subnautica.DevTools*.dll";
        Loader.FileWatcher.Changed += Loader.FileWatcherOnChanged;
        Loader.FileWatcher.Created += Loader.FileWatcherOnChanged;
        Loader.FileWatcher.EnableRaisingEvents = true;
    }

	public static void FileWatcherOnChanged(object source, FileSystemEventArgs e)
	{
        if (Loader.IsRunningFileWatcher)
            return;
        Loader.IsRunningFileWatcher = true;
        CoroutineHost.StartCoroutine(Loader.LoadDevToolsAsync(e.FullPath));
	}

	private static IEnumerator LoadDevToolsAsync(string filePath)
	{
            yield return new WaitForSeconds(0.1f);
            Loader.FileWatcher.EnableRaisingEvents = false;
            string[] strArray = Directory.GetFiles(Paths.GetGamePluginsPath(), "*.dll");
            for (int index = 0; index < strArray.Length; ++index)
            {
                string item = strArray[index];
                if (item.Contains("Subnautica.DevTools"))
                {
                    if (item != filePath)
                        File.Delete(item);
                    item = null;
                }
            }
            strArray = null;
            Log.Info("Reloading Dev Tools: " + filePath);
            try
            {
                Loader.LoadDevTools();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ReloadPlugins err: {0}", ex));
            }
            Log.Info("Reloaded Dev Tools...");
            Loader.IsRunningFileWatcher = false;
            Loader.FileWatcher.EnableRaisingEvents = true;
        }

        public static SubnauticaPlugin GetPlugin(Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsPublic && type.BaseType.Name.Contains("SubnauticaPlugin"))
                        return (SubnauticaPlugin)Activator.CreateInstance(type);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was a problem loading the plugin: {0}, Exception: {1}", (object)assembly.GetName().Name, (object)ex));
            }
            return null;
        }

        public static Assembly LoadAssembly(string assemblyPath)
	    {
		    try
		    {
			    return Assembly.Load(File.ReadAllBytes(assemblyPath));
		    }
		    catch (Exception ex)
		    {
			    Log.Error(string.Format("There was a problem loading the assembly: {0}, Exception: {1}", assemblyPath, ex));
		    }
		    return null;
	    }

	    private static List<string> SkippedDependencies = new List<string> { "DiscordRPCNativeNamedPipe", "Subnautica.Core" };
	    private static List<string> SkippedPlugins = new List<string> { "Subnautica.DevTools" };
}
