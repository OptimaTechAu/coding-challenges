namespace RobotWars.Core.Models
{
    public class Arena
    {
        public int Width { get; }
        public int Height { get; }

        public Arena(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}