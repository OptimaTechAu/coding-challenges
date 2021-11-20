using NUnit.Framework;
using RobotWars.Logic;

namespace RobotWars.Test
{
    public class RobotWarsGameTests
    {
        private RobotWarsGame _game;

        [SetUp]
        public void SetUp()
        {
            var inputParser = new InputParser();
            _game = new RobotWarsGame(inputParser);
        }

        [Test]
        public void Start_TestStartStates()
        {
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }

        [Test]
        public void Start_ValidInput_Initialized()
        {
            var result = _game.ProcessInstruction("5 4");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.Null);
            Assert.That(result.FailureMessage, Is.Null);

            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddOrSelectRobot));
            Assert.That(_game.ArenaWidth, Is.EqualTo(5));
            Assert.That(_game.ArenaHeight, Is.EqualTo(4));
        }

        [Test]
        public void Start_InvalidInput_NotInitialized()
        {
            var result = _game.ProcessInstruction("5");

            Assert.That(result.Successful, Is.False);
            Assert.That(result.FailureMessage, Is.Not.Null);

            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }
    }
}
