namespace CommanderLucyPluginInterface
{
    /// <summary>
    ///     Pluginname have to end on 'Plugin'.
    /// </summary>
    public interface ICommanderPlugin
    {
        void Execute(string[] parameters);
    }
}