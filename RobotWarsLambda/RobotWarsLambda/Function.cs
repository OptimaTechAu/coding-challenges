using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RobotWars.Application.Validators;
using RobotWars.Domain;
using FluentValidation;
using RobotWars.Application;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RobotWars.Presentation;

public class Function
{
    private readonly IRobotService _robotService;
    private readonly ILogger<Function> _logger;
    private readonly IValidator<Robot> _robotValidator;

    public Function()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(config => config.AddLambdaLogger())
            .AddSingleton<IRobotService, RobotService>()
            .AddSingleton<IValidator<Robot>, RobotValidator>()
            .BuildServiceProvider();

        _robotService = serviceProvider.GetRequiredService<IRobotService>();
        _robotValidator = serviceProvider.GetRequiredService<IValidator<Robot>>();
        _logger = serviceProvider.GetRequiredService<ILogger<Function>>();
    }

    public string FunctionHandler(string input, ILambdaContext context)
    {
        _logger.LogInformation($"Received input: {input}");

        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _logger.LogError("Input is empty.");
                return "Error: Input cannot be empty.";
            }

            string[] lines = input.Split("\n");
            var gridSize = lines[0].Split(" ");
            if (gridSize.Length < 2)
            {
                _logger.LogError("Invalid grid size.");
                return "Error: Invalid grid size.";
            }

            int maxX = int.Parse(gridSize[0]);
            int maxY = int.Parse(gridSize[1]);

            var results = new List<string>();

            for (int i = 1; i < lines.Length; i += 2)
            {
                var position = lines[i].Split(" ");
                if (position.Length < 3)
                {
                    _logger.LogError($"Invalid robot position: {lines[i]}");
                    return $"Error: Invalid robot position - {lines[i]}";
                }

                int x = int.Parse(position[0]);
                int y = int.Parse(position[1]);
                if (!Enum.TryParse<Direction>(position[2], true, out var facing))
                {
                    _logger.LogError($"Invalid direction: {position[2]}");
                    return $"Error: Invalid direction - {position[2]}";
                }

                var robot = new Robot(x, y, facing);

                var validationResult = _robotValidator.Validate(robot);
                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Validation failed: {string.Join(", ", validationResult.Errors)}");
                    return $"Error: {string.Join(", ", validationResult.Errors)}";
                }

                var commands = lines[i + 1];
                robot = _robotService.ExecuteCommands(robot, commands, maxX, maxY);
                results.Add(robot.ToString());
            }

            string output = string.Join("\n", results);
            _logger.LogInformation($"Final Output: {output}");
            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occurred: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }
}
