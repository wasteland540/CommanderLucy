using System.Collections.Generic;

namespace CommanderLucy.Services
{
    public interface IPluginService
    {
        List<string> GetPluginList();

        void AddPlugin(string pluginPath);

        void DeletePlugin(string pluginPath);

        void ExecutePlugin(string name, string[] parameters);
    }
}