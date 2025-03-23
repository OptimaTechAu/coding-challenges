using Microsoft.Extensions.Logging;
using RobotWars.Domain;

namespace RobotWars.Application
{
    public class RobotService : IRobotService
    {
        private readonly ILogger<RobotService> _logger;

        public RobotService(ILogger<RobotService> logger)
        {
            _logger = logger;
        }

        public Robot ExecuteCommands(Robot robot, string commands, int maxX, int maxY)
        {
            if (robot == null)
            {
                _logger.LogError("Robot instance is null.");
                throw new ArgumentNullException(nameof(robot), "Robot cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(commands))
            {
                _logger.LogWarning("Received empty command string.");
                return robot;
            }

            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        robot.RotateLeft();
                        break;
                    case 'R':
                        robot.RotateRight();
                        break;
                    case 'M':
                        robot.MoveForward(maxX, maxY);
                        break;
                    default:
                        _logger.LogError($"Invalid command encountered: {command}");
                        throw new ArgumentException($"Invalid command: {command}");
                }
            }

            _logger.LogInformation($"Robot final position: {robot}");
            return robot;
        }
    }

}
