using RobotWars.Core.Models;

namespace RobotWars.Core.Interfaces
{
    public interface ICommandProcessor
    {
        string ProcessCommands(string input);
        void ExecuteCommand(Robot robot, Arena arena, char command);
        void ExecuteCommands(Robot robot, Arena arena, string commands);
    }
}