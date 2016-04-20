using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommanderLucy.Messages;
using CommanderLucyPluginInterface;
using GalaSoft.MvvmLight.Messaging;

namespace CommanderLucy.Services
{
    public class PluginService : IPluginService
    {
        private const string PluginFolderName = "Plugins";
        private const string PluginExtension = ".dll";
        private readonly IMessenger _messenger;

        public PluginService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public List<string> GetPluginList()
        {
            return Directory.GetFiles(PluginFolderName)
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                .Select(n => Path.GetFileName(n).Replace(Path.GetExtension(n), ""))
                .ToList();
        }

        public void AddPlugin(string pluginPath)
        {
            if (!Directory.Exists(PluginFolderName))
            {
                Directory.CreateDirectory(PluginFolderName);
            }

            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                File.Copy(pluginPath, Path.Combine(PluginFolderName, Path.GetFileName(pluginPath)));

                _messenger.Send(new PluginAddedMsg());
            }
            catch (Exception e)
            {
                _messenger.Send(new CannotAddPluginMsg(e.Message));
            }
        }

        public void DeletePlugin(string pluginPath)
        {
            if (Directory.Exists(PluginFolderName) && !string.IsNullOrEmpty(pluginPath))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                string filePath = Path.Combine(PluginFolderName, Path.GetFileName(pluginPath)) +
                                  PluginExtension;
                File.Delete(filePath);

                _messenger.Send(new PluginDeletedMsg());
            }
        }

        public void ExecutePlugin(string name, string[] parameters)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string filePath =
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), PluginFolderName,
                    Path.GetFileName(name)) +
                PluginExtension;

            Assembly assembly = Assembly.LoadFile(filePath);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && type.GetInterfaces().ToList().Contains(typeof (ICommanderPlugin)))
                {
                    var plugin = Activator.CreateInstance(type) as ICommanderPlugin;

                    if (plugin != null)
                    {
                        plugin.Execute(parameters);
                    }
                }
            }
        }
    }
}