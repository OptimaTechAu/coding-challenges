using System;
using System.Collections.Generic;
using System.IO;
using RobotWars.Core.Interfaces;
using RobotWars.Core.Models;
using RobotWars.Core.Exceptions;

namespace RobotWars.Core.Services
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IRobotService _robotService;
        private readonly IArenaService _arenaService;

        public CommandProcessor(IRobotService robotService, IArenaService arenaService)
        {
            _robotService = robotService;
            _arenaService = arenaService;
        }

        public string ProcessCommands(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    throw new RobotWarsException("Input cannot be empty.");

                using var reader = new StringReader(input);

                // Read the first line (Arena dimensions)
                var arenaSize = reader.ReadLine()?.Trim().Split(' ');

                if (arenaSize == null || arenaSize.Length != 2 ||
                    !int.TryParse(arenaSize[0], out int width) ||
                    !int.TryParse(arenaSize[1], out int height))
                {
                    throw new RobotWarsException("Invalid arena dimensions. Expected format: '5 5'");
                }

                var arena = _arenaService.CreateArena(width, height);
                var results = new List<string>();

                string positionLine;
                while ((positionLine = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(positionLine))
                        continue;

                    var position = positionLine.Trim().Split(' ');
                    if (position.Length != 3 ||
                        !int.TryParse(position[0], out int x) ||
                        !int.TryParse(position[1], out int y) ||
                        !Enum.TryParse<Direction>(position[2], out Direction direction))
                    {
                        throw new RobotWarsException($"Invalid robot position: {positionLine}");
                    }

                    var robot = new Robot(x, y, direction);

                    var commands = reader.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(commands))
                    {
                        ExecuteCommands(robot, arena, commands);
                    }

                    results.Add(robot.ToString());
                }

                return string.Join("\n", results);
            }
            catch (RobotWarsException ex)
            {
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Unexpected error processing commands: {ex.Message}";
            }
        }

        // ✅ Ensure `ExecuteCommand` is PUBLIC and Matches Interface Signature
        public void ExecuteCommand(Robot robot, Arena arena, char command)
        {
            switch (command)
            {
                case 'L':
                    _robotService.TurnLeft(robot);
                    break;
                case 'R':
                    _robotService.TurnRight(robot);
                    break;
                case 'M':
                    _robotService.Move(robot, arena);
                    break;
                default:
                    throw new RobotWarsException($"Invalid command: {command}");
            }
        }

        public void ExecuteCommands(Robot robot, Arena arena, string commands)
        {
            foreach (char command in commands)
            {
                ExecuteCommand(robot, arena, command);
            }
        }
    }
}
