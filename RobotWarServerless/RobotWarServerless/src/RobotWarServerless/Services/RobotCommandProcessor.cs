using RobotWarServerless.Models;

namespace RobotWarServerless.Services
{
    public class RobotCommandProcessor : IRobotCommandProcessor
    {
        public IEnumerable<string> Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be empty");

            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Set Arena data
            var arena = ParseArena(lines[0]);
            var results = new List<string>();

            // Process robot command with position and move
            for (int i = 1; i < lines.Length; i += 2)
            {
                var robot = ParseRobot(lines[i], arena);
                var commands = lines[i + 1];
                robot.Execute(commands);
                results.Add(robot.ToString());
            }

            return results;
        }

        private Arena ParseArena(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var width) || !int.TryParse(parts[1], out var height))
                throw new ArgumentException("Invalid arena dimensions format");

            return new Arena(width, height);
        }

        private Robot ParseRobot(string positionInput, Arena arena)
        {
            var parts = positionInput.Split(' ');
            if (parts.Length != 3 || !int.TryParse(parts[0], out var x) || !int.TryParse(parts[1], out var y))
                throw new ArgumentException("Invalid robot position format");

            if (!Enum.TryParse(parts[2], out Orientation orientation))
                throw new ArgumentException("Invalid robot orientation");

            return new Robot(x, y, orientation, arena);
        }
    }
}
