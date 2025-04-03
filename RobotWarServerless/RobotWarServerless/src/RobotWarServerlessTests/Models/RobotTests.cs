using Xunit;

namespace RobotWarServerless.Models.Tests
{
    public class RobotTests
    {
        private readonly Arena _arena = new Arena(5, 5);

        [Theory]
        [InlineData("0 0 E", "M", "1 0 E")]
        [InlineData("1 1 S", "M", "1 0 S")]
        [InlineData("1 2 W", "M", "0 2 W")]
        [InlineData("1 3 N", "M", "1 4 N")]
        public void MoveForward_Correctly(string startPos, string commands, string expectedPos)
        {
            var robot = CreateRobot(startPos);
            robot.Execute(commands);
            Xunit.Assert.Equal(expectedPos, robot.ToString());
        }

        [Theory]
        [InlineData("0 0 E", "L", "0 0 N")]
        [InlineData("0 0 S", "L", "0 0 E")]
        [InlineData("0 0 W", "L", "0 0 S")]
        [InlineData("0 0 N", "L", "0 0 W")]
        public void TurnLeft_Correctly(string startPos, string commands, string expectedPos)
        {
            var robot = CreateRobot(startPos);
            robot.Execute(commands);
            Xunit.Assert.Equal(expectedPos, robot.ToString());
        }

        [Theory]
        [InlineData("0 0 E", "R", "0 0 S")]
        [InlineData("0 0 S", "R", "0 0 W")]
        [InlineData("0 0 W", "R", "0 0 N")]
        [InlineData("0 0 N", "R", "0 0 E")]
        public void TurnRight_Correctly(string startPos, string commands, string expectedPos)
        {
            var robot = CreateRobot(startPos);
            robot.Execute(commands);
            Xunit.Assert.Equal(expectedPos, robot.ToString());
        }

        private Robot CreateRobot(string position)
        {
            var parts = position.Split(' ');
            return new Robot(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                Enum.Parse<Orientation>(parts[2]),
                _arena);
        }
    }
}