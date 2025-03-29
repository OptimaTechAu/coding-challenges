using NUnit.Framework;
using RobotWars.Logic;
using RobotWars.Logic.Navigation;
using RobotWars.Logic.Parsing;

namespace RobotWars.Test
{
    public class RobotWarsGameTests
    {
        private IRobotWarsGame _game;

        [SetUp]
        public void SetUp()
        {
            var inputParser = new InputParser();
            var robotNavigator = new RobotNavigator();
            _game = new RobotWarsGame(inputParser, robotNavigator);
        }

        [Test]
        public void Start_TestStartStates()
        {
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }

        [Test]
        public void Start_InvalidInput_NotInitialized()
        {
            var result = ((IRobotWarsGame)_game).ProcessInstruction("5");

            Assert.That(result.Successful, Is.False);
            Assert.That(result.FailureMessage, Is.Not.Null);

            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }

        [Test]
        public void PlaySampleInput_Works()
        {
            var result = ((IRobotWarsGame)_game).ProcessInstruction("5 4");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddRobot));
            Assert.That(_game.ArenaWidth, Is.EqualTo(5));
            Assert.That(_game.ArenaHeight, Is.EqualTo(4));
            CollectionAssert.IsEmpty(_game.GetRobots());


            result = ((IRobotWarsGame)_game).ProcessInstruction("1 2 N");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.MoveRobot));
            CollectionAssert.AreEqual(
                new[] {
                    new Robot(1, 2, RobotHeading.North)
                },
                _game.GetRobots()
            );


            result = ((IRobotWarsGame)_game).ProcessInstruction("LMLMLMLMM");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddRobot));
            CollectionAssert.AreEqual(
                new[] {
                    new Robot(1, 3, RobotHeading.North)
                },
                _game.GetRobots()
            );


            result = ((IRobotWarsGame)_game).ProcessInstruction("3 3 E");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.MoveRobot));
            CollectionAssert.AreEqual(
                new[] {
                    new Robot(1, 3, RobotHeading.North),
                    new Robot(3, 3, RobotHeading.East)
                },
                _game.GetRobots()
            );


            result = ((IRobotWarsGame)_game).ProcessInstruction("MMRMMRMRRM");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddRobot));
            CollectionAssert.AreEqual(
                new[] {
                    new Robot(1, 3, RobotHeading.North),
                    new Robot(5, 1, RobotHeading.East)
                },
                _game.GetRobots()
            );
        }
    }
}
