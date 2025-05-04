using RobotWars.Core.Interfaces;
using RobotWars.Core.Models;

namespace RobotWars.Core.Services
{
    public class ArenaService : IArenaService
    {
        public Arena CreateArena(int width, int height)
        {
            return new Arena(width, height);
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && y >= 0;
        }
    }
}