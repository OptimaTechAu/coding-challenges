using Moq;
using Microsoft.Extensions.Logging;
using RobotWars.Domain;
using Xunit;
using RobotWars.Application;
using FluentAssertions;

namespace RobotWars.Tests;

public class RobotServiceTests
{
    private readonly IRobotService _robotService;

    public RobotServiceTests()
    {
        var mockLogger = new Mock<ILogger<RobotService>>();
        _robotService = new RobotService(mockLogger.Object);
    }

    [Fact]
    public void Robot_Should_Move_Correctly()
    {
        var robot = new Robot(1, 2, Direction.North);
        var result = _robotService.ExecuteCommands(robot, "LMLMLMLMM", 5, 5);

        result.X.Should().Be(1);
        result.Y.Should().Be(3);
        result.Facing.Should().Be(Direction.North);
    }

    [Fact]
    public void Robot_Should_Ignore_Invalid_Movement()
    {
        var robot = new Robot(0, 0, Direction.South);
        var result = _robotService.ExecuteCommands(robot, "MMMMM", 5, 5);

        result.X.Should().Be(0);
        result.Y.Should().Be(0);
        result.Facing.Should().Be(Direction.South);
    }

    [Fact]
    public void Robot_Should_Throw_Exception_On_Invalid_Command()
    {
        var robot = new Robot(2, 2, Direction.East);

        var act = () => _robotService.ExecuteCommands(robot, "ABCD", 5, 5);

        act.Should().Throw<ArgumentException>().WithMessage("Invalid command: A");
    }
}
