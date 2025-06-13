using Amazon.Lambda.Core;
using System.Text.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace serverless_robot_wars;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Direction { N, E, S, W }
public record Position(int X, int Y);

public class Function
{
    public string RobotNavigationHandler(InputData input, ILambdaContext context)
    {
        // Log input parameters for debugging and auditing
        context.Logger.LogInformation($"Received input: Arena size = {input.MaxX}x{input.MaxY}, Robots count = {input.Robots.Count}");

        // Initialize the battle arena with given dimensions
        var arena = new Arena(input.MaxX, input.MaxY);
        var results = new List<string>();

        // Process each robot sequentially
        foreach (var robotInput in input.Robots)
        {
            // Initialize robot at starting position
            var robot = new Robot(
                new Position(robotInput.StartX, robotInput.StartY),
                robotInput.Direction,
                arena.MaxX,
                arena.MaxY);

            // Execute all commands for this robot
            robot.ExecuteCommands(robotInput.Commands);
            results.Add(robot.ToString());
        }

        // Log final positions for verification
        context.Logger.LogInformation($"Final output: \n{results}");

        return string.Join("\n", results);
    }

    public class Arena
    {
        public int MaxX { get; }
        public int MaxY { get; }

        public Arena(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }
    }

    public class Robot
    {
        public Position Position { get; private set; }
        public Direction Orientation { get; private set; }
        private readonly int _maxX;
        private readonly int _maxY;

        private static readonly Dictionary<Direction, Position> Movements = new()
        {
            [Direction.N] = new(0, 1),
            [Direction.E] = new(1, 0),
            [Direction.S] = new(0, -1),
            [Direction.W] = new(-1, 0)
        };

        private static readonly Dictionary<char, Dictionary<Direction, Direction>> Rotations = new()
        {
            ['L'] = new() // Left turn mappings
            {
                [Direction.N] = Direction.W,
                [Direction.W] = Direction.S,
                [Direction.S] = Direction.E,
                [Direction.E] = Direction.N
            },
            ['R'] = new() // Right turn mappings
            {
                [Direction.N] = Direction.E,
                [Direction.E] = Direction.S,
                [Direction.S] = Direction.W,
                [Direction.W] = Direction.N
            }
        };

        public Robot(Position position, Direction orientation, int maxX, int maxY)
        {
            Position = position;
            Orientation = orientation;
            _maxX = maxX;
            _maxY = maxY;
        }

        public void ExecuteCommands(string commands)
        {
            foreach (var command in commands)
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(char command)
        {
            if (Rotations.TryGetValue(command, out var rotationMap))
            {
                Orientation = rotationMap[Orientation];
            }
            else if (command == 'M')
            {
                var movement = Movements[Orientation];
                var newX = Position.X + movement.X;
                var newY = Position.Y + movement.Y;

                if (newX >= 0 && newX <= _maxX &&
                   newY >= 0 && newY <= _maxY)
                {
                    Position = new Position(newX, newY);
                }
            }
        }

        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Orientation}";
        }
    }

    public class InputData
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public required List<RobotInput> Robots { get; set; }
    }

    public class RobotInput
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public Direction Direction { get; set; }
        public required string Commands { get; set; }
    }
}
