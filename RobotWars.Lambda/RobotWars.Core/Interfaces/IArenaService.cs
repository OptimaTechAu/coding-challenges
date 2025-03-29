using RobotWars.Core.Models;

namespace RobotWars.Core.Interfaces
{
    public interface IArenaService
    {
        bool IsValidPosition(int x, int y);
        Arena CreateArena(int width, int height);
    }
}