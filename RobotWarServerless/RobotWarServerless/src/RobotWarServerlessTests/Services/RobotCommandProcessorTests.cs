using Xunit;

namespace RobotWarServerless.Services.Tests
{
    public class RobotCommandProcessorTests
    {
        [Fact]
        public void ProcessCommands_WithValidInput_ReturnsCorrectPositions()
        {
            var input = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

            var processor = new RobotCommandProcessor();
            var results = processor.Process(input).ToList();

            Xunit.Assert.Equal(2, results.Count);
            Xunit.Assert.Equal("1 3 N", results[0]);
            Xunit.Assert.Equal("5 1 E", results[1]);
        }
    }
}