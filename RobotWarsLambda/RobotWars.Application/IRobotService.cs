using RobotWars.Domain;

namespace RobotWars.Application
{
    public interface IRobotService
    {
        Robot ExecuteCommands(Robot robot, string commands, int maxX, int maxY);
    }
}
