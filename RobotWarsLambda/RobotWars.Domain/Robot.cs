namespace RobotWars.Domain
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Facing { get; private set; }

        public Robot(int x, int y, Direction facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }

        public void RotateLeft()
        {
            Facing = Facing switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => Facing
            };
        }

        public void RotateRight()
        {
            Facing = Facing switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => Facing
            };
        }

        public void MoveForward(int maxX, int maxY)
        {
            switch (Facing)
            {
                case Direction.North when Y < maxY:
                    Y++;
                    break;
                case Direction.South when Y > 0:
                    Y--;
                    break;
                case Direction.East when X < maxX:
                    X++;
                    break;
                case Direction.West when X > 0:
                    X--;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{X} {Y} {Facing.ToString()[0]}"; 
        }
    }
}
