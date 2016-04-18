using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommanderLucy.Model;

namespace CommanderLucy.Services
{
    public class ConfigService : IConfigService
    {
        private const string ConfigFilename = "CommandConfig.xml";

        public List<Command> LoadConfig()
        {
            if (File.Exists(ConfigFilename))
            {
                return Command.Deserialize(ConfigFilename).ToList();
            }

            return new List<Command>
            {
                new Command
                {
                    Name = "Hello Master",
                    CommandText = "Hello",
                    Type = CommandType.Basic,
                    Action = "https://github.com/wasteland540/CommanderLucy"
                }
            };
        }

        public void SaveConfig(List<Command> commands)
        {
            Command.Serialize(ConfigFilename, commands.ToArray());
        }

        public void AddCommand(Command command)
        {
            if (File.Exists(ConfigFilename))
            {
                var commands = Command.Deserialize(ConfigFilename).ToList();
                commands.Add(command);

                SaveConfig(commands);
            }
        }

        public void UpdateCommand(Command command)
        {
            if (File.Exists(ConfigFilename))
            {
                var commands = Command.Deserialize(ConfigFilename).ToList();

                var oldCommand = commands.FirstOrDefault(c => c.Name == command.Name);

                if (oldCommand != null)
                {
                    commands.Remove(oldCommand);
                    commands.Add(command);
                }

                SaveConfig(commands);
            }
        }
    }
}