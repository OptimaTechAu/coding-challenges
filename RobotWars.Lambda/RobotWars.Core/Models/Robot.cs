namespace RobotWars.Core.Models
{
    public class Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public Robot(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Direction}";
        }
    }
}