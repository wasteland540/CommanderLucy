using System;
using System.IO;
using System.Text;
using CommanderLucyPluginInterface;

namespace TestCommanderPlugin
{
    /// <summary>
    ///     Pluginname have to end on 'Plugin'.
    /// </summary>
    public class TestCommanderPlugin : ICommanderPlugin
    {
        public void Execute(string[] parameters)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var files = Directory.GetFiles(path);

            var fileList = new StringBuilder();

            foreach (string file in files)
            {
                fileList.AppendLine(file);
            }

            File.WriteAllText(Path.Combine(path, "TestCommanderPlugin.txt"), fileList.ToString());
        }
    }
}