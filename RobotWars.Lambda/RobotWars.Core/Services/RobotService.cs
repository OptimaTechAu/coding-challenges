using RobotWars.Core.Interfaces;
using RobotWars.Core.Models;

namespace RobotWars.Core.Services
{
    public class RobotService : IRobotService
    {
        public void TurnLeft(Robot robot)
        {
            robot.Direction = robot.Direction switch
            {
                Direction.N => Direction.W,
                Direction.E => Direction.N,
                Direction.S => Direction.E,
                Direction.W => Direction.S,
                _ => throw new ArgumentException($"Invalid direction: {robot.Direction}")
            };
        }

        public void TurnRight(Robot robot)
        {
            robot.Direction = robot.Direction switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentException($"Invalid direction: {robot.Direction}")
            };
        }

        public void Move(Robot robot, Arena arena)
        {
            int newX = robot.X;
            int newY = robot.Y;

            switch (robot.Direction)
            {
                case Direction.N:
                    newY += 1;
                    break;
                case Direction.E:
                    newX += 1;
                    break;
                case Direction.S:
                    newY -= 1;
                    break;
                case Direction.W:
                    newX -= 1;
                    break;
            }

            if (IsValidPosition(newX, newY, arena))
            {
                robot.X = newX;
                robot.Y = newY;
            }
        }

        private bool IsValidPosition(int x, int y, Arena arena)
        {
            return x >= 0 && x <= arena.Width && y >= 0 && y <= arena.Height;
        }
    }
}
