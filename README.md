# CommanderLucy

#### What is it?
A tool for speech recognition like Cortana, Siri or Google Now.
It's configureable for your own usecases. You can simple run processes like open a link in your default browser or just start notepad.
You also can write your own plugins!

#### Screenshots

![Main Window](https://github.com/wasteland540/CommanderLucy/tree/master/Screenshots/MainWindow.png) 
![Config Manager](https://github.com/wasteland540/CommanderLucy/tree/master/Screenshots/ConfigManager.png) 
![Basic Command](https://github.com/wasteland540/CommanderLucy/tree/master/Screenshots/BasicCommand.png) 
![Plugin Command](https://github.com/wasteland540/CommanderLucy/tree/master/Screenshots/PluginCommand.png) 
![Plugin Manager](https://github.com/wasteland540/CommanderLucy/tree/master/Screenshots/PluginManager.png) 

#### Plugins
If you want to write your own plugin, just do it. All you need is to implement the following interface.
```
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
```
As you read in the summary, your Plugin classname have to end with 'Plugin'. And that's all you need for writing a plugin.
You can check out the simple [TestPlugin] (https://github.com/wasteland540/CommanderLucy/tree/master/TestCommanderPlugin).

If you have any suggestions feel free to contact me.
