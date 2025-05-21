namespace RobotWarServerless.Models
{
    public class Robot
    {
        private int X { get; set; }
        private int Y { get; set; }
        private Orientation Orientation { get; set; }
        private readonly Arena _arena;

        public Robot(int x, int y, Orientation orientation, Arena arena)
        {
            if (!arena.IsPositionValid(x, y))
                throw new ArgumentException("Initial position is outside arena bounds");

            X = x;
            Y = y;
            Orientation = orientation;
            _arena = arena;
        }

        public void Execute(string commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'L': TurnLeft(); break;
                    case 'R': TurnRight(); break;
                    case 'M': MoveForward(); break;
                    default: throw new ArgumentException($"Invalid command: {command}");
                }
            }
        }

        private void TurnLeft()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
            };
        }

        private void TurnRight()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
            };
        }

        private void MoveForward()
        {
            var (newX, newY) = Orientation switch
            {
                Orientation.N => (X, Y + 1),
                Orientation.E => (X + 1, Y),
                Orientation.S => (X, Y - 1),
                Orientation.W => (X - 1, Y),
            };

            if (!_arena.IsPositionValid(newX, newY))
                throw new InvalidOperationException("Movement would take robot outside arena bounds");

            X = newX;
            Y = newY;
        }

        public override string ToString() => $"{X} {Y} {Orientation}";
    }
}
