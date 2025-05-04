using RobotWars.Core.Models;

namespace RobotWars.Core.Interfaces
{
    public interface IRobotService
    {
        void TurnLeft(Robot robot);
        void TurnRight(Robot robot);
        void Move(Robot robot, Arena arena);
    }
}