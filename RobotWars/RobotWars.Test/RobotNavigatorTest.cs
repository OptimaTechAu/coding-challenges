using NUnit.Framework;
using RobotWars.Logic;
using RobotWars.Logic.Navigation;
using System.Collections.Generic;

namespace RobotWars.Test
{
    [TestFixture]
    public class RobotNavigatorTest
    {
        [TestCaseSource(nameof(PlanMoveTestCases))]
        public PlanMoveResult PlanMove(Robot robot, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.PlanMove(robot, move);
        }

        private static IEnumerable<TestCaseData> PlanMoveTestCases()
        {
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.LeftRotate)
                .Returns(new PlanMoveResult { MovedPosition = false, Robot = new Robot(1, 1, RobotHeading.West) });
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.RightRotate)
                .Returns(new PlanMoveResult { MovedPosition = false, Robot = new Robot(1, 1, RobotHeading.East) });

            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Robot = new Robot(1, 2, RobotHeading.North) });
            
            yield return new TestCaseData(new Robot(0, 0, RobotHeading.West), RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Robot = new Robot(-1, 0, RobotHeading.West) });
            yield return new TestCaseData(new Robot(0, 0, RobotHeading.South), RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Robot = new Robot(0, -1, RobotHeading.South) });
        }

        [TestCase(RobotHeading.North, RobotMove.MoveOneStep, ExpectedResult = RobotHeading.North)]
        [TestCase(RobotHeading.North, RobotMove.LeftRotate, ExpectedResult = RobotHeading.West)]
        [TestCase(RobotHeading.West, RobotMove.LeftRotate, ExpectedResult = RobotHeading.South)]        
        [TestCase(RobotHeading.South, RobotMove.LeftRotate, ExpectedResult = RobotHeading.East)]        
        [TestCase(RobotHeading.East, RobotMove.LeftRotate, ExpectedResult = RobotHeading.North)]
        [TestCase(RobotHeading.North, RobotMove.RightRotate, ExpectedResult = RobotHeading.East)]
        [TestCase(RobotHeading.East, RobotMove.RightRotate, ExpectedResult = RobotHeading.South)]
        [TestCase(RobotHeading.South, RobotMove.RightRotate, ExpectedResult = RobotHeading.West)]
        [TestCase(RobotHeading.West, RobotMove.RightRotate, ExpectedResult = RobotHeading.North)]        
        public RobotHeading Rotate(RobotHeading heading, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.Rotate(heading, move);
        }

        [TestCaseSource(nameof(MoveTestCases))]
        public Robot Move(Robot robot, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.Move(robot, move);
        }

        private static IEnumerable<TestCaseData> MoveTestCases()
        {
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.LeftRotate)
                .Returns(new Robot(1, 1, RobotHeading.North));
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.RightRotate)
                .Returns(new Robot(1, 1, RobotHeading.North));

            yield return new TestCaseData(new Robot(1, 1, RobotHeading.North), RobotMove.MoveOneStep)
                .Returns(new Robot(1, 2, RobotHeading.North));
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.West), RobotMove.MoveOneStep)
                .Returns(new Robot(0, 1, RobotHeading.West));
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.South), RobotMove.MoveOneStep)
                .Returns(new Robot(1, 0, RobotHeading.South));
            yield return new TestCaseData(new Robot(1, 1, RobotHeading.East), RobotMove.MoveOneStep)
                .Returns(new Robot(2, 1, RobotHeading.East));
        }
    }
}
