using System.Collections.Generic;
using CommanderLucy.Model;

namespace CommanderLucy.Services
{
    public interface IConfigService
    {
        List<Command> LoadConfig();

        void SaveConfig(List<Command> commands);

        void AddCommand(Command command);

        void UpdateCommand(Command command);
    }
}