namespace CommanderLucy.Messages
{
    public class ChoosePluginFileResponseMsg
    {
        public string PluginFilename { get; private set; }

        public ChoosePluginFileResponseMsg(string pluginFilename)
        {
            PluginFilename = pluginFilename;
        }
    }
}