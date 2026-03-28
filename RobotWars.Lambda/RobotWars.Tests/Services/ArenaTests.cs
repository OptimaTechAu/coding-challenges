using RobotWars.Core.Interfaces;
using RobotWars.Core.Models;
using RobotWars.Core.Services;
using Xunit;

namespace RobotWars.Tests.Unit
{
    public class ArenaServiceTests
    {
        private readonly IArenaService _arenaService;

        public ArenaServiceTests()
        {
            _arenaService = new ArenaService();
        }

        [Fact]
        public void CreateArena_WithValidDimensions_ShouldReturnArenaInstance()
        {          
            int width = 5;
            int height = 5;
            
            Arena arena = _arenaService.CreateArena(width, height);
           
            Assert.NotNull(arena);
            Assert.Equal(width, arena.Width);
            Assert.Equal(height, arena.Height);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(5, 5, true)]
        [InlineData(-1, 0, false)]
        [InlineData(0, -1, false)]
        [InlineData(-5, -5, false)]
        public void IsValidPosition_ShouldReturnCorrectValidation(int x, int y, bool expected)
        {           
            bool isValid = _arenaService.IsValidPosition(x, y);
      
            Assert.Equal(expected, isValid);
        }
    }
}
