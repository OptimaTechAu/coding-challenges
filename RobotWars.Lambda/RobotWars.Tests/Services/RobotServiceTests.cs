using Xunit;
using RobotWars.Core.Models;
using RobotWars.Core.Services;

namespace RobotWars.Tests.Unit
{
    public class RobotServiceTests
    {
        private readonly RobotService _robotService;

        public RobotServiceTests()
        {
            _robotService = new RobotService();
        }

        [Fact]
        public void TurnLeft_ChangesDirectionCorrectly()
        {          
            var robot = new Robot(0, 0, Direction.N);
            
            _robotService.TurnLeft(robot);
            Assert.Equal(Direction.W, robot.Direction);

            _robotService.TurnLeft(robot);
            Assert.Equal(Direction.S, robot.Direction);

            _robotService.TurnLeft(robot);
            Assert.Equal(Direction.E, robot.Direction);

            _robotService.TurnLeft(robot);
            Assert.Equal(Direction.N, robot.Direction);
        }

        [Fact]
        public void TurnRight_ChangesDirectionCorrectly()
        {          
            var robot = new Robot(0, 0, Direction.N);
            
            _robotService.TurnRight(robot);
            Assert.Equal(Direction.E, robot.Direction);

            _robotService.TurnRight(robot);
            Assert.Equal(Direction.S, robot.Direction);

            _robotService.TurnRight(robot);
            Assert.Equal(Direction.W, robot.Direction);

            _robotService.TurnRight(robot);
            Assert.Equal(Direction.N, robot.Direction);
        }

        [Fact]
        public void Move_WithValidPosition_MovesRobot()
        {           
            var arena = new Arena(5, 5);
            var robot = new Robot(2, 2, Direction.N);
         
            _robotService.Move(robot, arena);
           
            Assert.Equal(2, robot.X);
            Assert.Equal(3, robot.Y);
        }

        [Fact]
        public void Move_WithInvalidPosition_DoesNotMoveRobot()
        {          
            var arena = new Arena(5, 5);
            var robot = new Robot(0, 0, Direction.S);
          
            _robotService.Move(robot, arena);
      
            Assert.Equal(0, robot.X);
            Assert.Equal(0, robot.Y);
        }
    }
}