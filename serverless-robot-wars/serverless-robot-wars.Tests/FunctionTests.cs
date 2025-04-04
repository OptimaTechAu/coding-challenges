using Amazon.Lambda.TestUtilities;
using static serverless_robot_wars.Function;

namespace serverless_robot_wars.Tests
{
    public class FunctionTests
    {
        [Fact]
        public void Handler_ProcessesMultipleRobots()
        {
            var input = new InputData
            {
                MaxX = 5,
                MaxY = 5,
                Robots = [new() { StartX = 1, StartY = 2, Direction = Direction.N, Commands = "LMLMLMLMM" }]
            };

            var context = new TestLambdaContext()
            {
                Logger = new TestLambdaLogger(),
                MemoryLimitInMB = 256,
                RemainingTime = TimeSpan.FromMinutes(1)
            };

            var result = new Function().RobotNavigationHandler(input, context);
            Console.WriteLine(result.ToString());
            Assert.Equal("1 3 N", result);
        }
    }
}