using Xunit;
using Moq;
using RobotWars.Core.Interfaces;
using RobotWars.Core.Models;
using RobotWars.Core.Services;

namespace RobotWars.Tests.Unit
{
    public class CommandProcessorTests
    {
        private readonly Mock<IRobotService> _mockRobotService;
        private readonly Mock<IArenaService> _mockArenaService;
        private readonly CommandProcessor _commandProcessor;

        public CommandProcessorTests()
        {
            _mockRobotService = new Mock<IRobotService>();
            _mockArenaService = new Mock<IArenaService>();
            _commandProcessor = new CommandProcessor(_mockRobotService.Object, _mockArenaService.Object);
        }

        [Fact]
        public void ProcessCommands_WithValidInput_ReturnsExpectedOutput()
        {            
            var arena = new Arena(5, 5);
            _mockArenaService.Setup(s => s.CreateArena(5, 5)).Returns(arena);
           
            _mockRobotService.Setup(s => s.TurnLeft(It.IsAny<Robot>()))
                .Callback<Robot>(r => {
                    if (r.Direction == Direction.N) r.Direction = Direction.W;
                    else if (r.Direction == Direction.W) r.Direction = Direction.S;
                    else if (r.Direction == Direction.S) r.Direction = Direction.E;
                    else if (r.Direction == Direction.E) r.Direction = Direction.N;
                });

            _mockRobotService.Setup(s => s.TurnRight(It.IsAny<Robot>()))
                .Callback<Robot>(r => {
                    if (r.Direction == Direction.N) r.Direction = Direction.E;
                    else if (r.Direction == Direction.E) r.Direction = Direction.S;
                    else if (r.Direction == Direction.S) r.Direction = Direction.W;
                    else if (r.Direction == Direction.W) r.Direction = Direction.N;
                });

            _mockRobotService.Setup(s => s.Move(It.IsAny<Robot>(), It.IsAny<Arena>()))
                .Callback<Robot, Arena>((r, a) => {
                    if (r.Direction == Direction.N) r.Y += 1;
                    else if (r.Direction == Direction.E) r.X += 1;
                    else if (r.Direction == Direction.S) r.Y -= 1;
                    else if (r.Direction == Direction.W) r.X -= 1;
                });

            var input = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
        
            var result = _commandProcessor.ProcessCommands(input);
           
            Assert.Equal("1 3 N\n5 1 E", result);
        }

        [Fact]
        public void ExecuteCommand_CallsCorrectServiceMethod()
        {           
            var robot = new Robot(0, 0, Direction.N);
            var arena = new Arena(5, 5);
           
            _commandProcessor.ExecuteCommand(robot, arena, 'L');
            _commandProcessor.ExecuteCommand(robot, arena, 'R');
            _commandProcessor.ExecuteCommand(robot, arena, 'M');
          
            _mockRobotService.Verify(s => s.TurnLeft(robot), Times.Once);
            _mockRobotService.Verify(s => s.TurnRight(robot), Times.Once);
            _mockRobotService.Verify(s => s.Move(robot, arena), Times.Once);
        }
    }
}