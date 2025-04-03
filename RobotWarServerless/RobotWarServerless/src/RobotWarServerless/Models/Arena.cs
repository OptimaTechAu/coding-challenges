namespace RobotWarServerless.Models
{
    public class Arena
    {
        private int Width { get; }
        private int Height { get; }

        public Arena(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Arena dimensions must be positive");

            Width = width;
            Height = height;
        }

        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x <= Width && y >= 0 && y <= Height;
        }
    }
}
