namespace RobotWars.Logic.Navigation
{
    public class RobotNavigator : IRobotNavigator
    {
        public PlanMoveResult PlanMove(Robot robot, RobotMove move)
        {
            if (move == RobotMove.LeftRotate || move == RobotMove.RightRotate)
            {
                var nextHeading = Rotate(robot.Heading, move);
                var updatedRobot = robot with { Heading = nextHeading };
                return PlanMoveResult.RotateResult(updatedRobot);
            }
            else
            {
                var updatedRobot = Move(robot, move);
                return PlanMoveResult.MoveResult(updatedRobot);
            }
        }

        public Robot Move(Robot robot, RobotMove move)
        {
            if (move == RobotMove.LeftRotate || move == RobotMove.RightRotate)
                return robot;

            Robot updatedRobot = robot;

            switch (robot.Heading)
            {
                case RobotHeading.North:
                    updatedRobot = robot with { Row = robot.Row + 1 };
                    break;
                case RobotHeading.West:
                    updatedRobot = robot with { Column = robot.Column - 1 };
                    break;
                case RobotHeading.South:
                    updatedRobot = robot with { Row = robot.Row - 1 };
                    break;
                case RobotHeading.East:
                    updatedRobot = robot with { Column = robot.Column + 1 };
                    break;
            }

            return updatedRobot;
        }

        public RobotHeading Rotate(RobotHeading robotHeading, RobotMove move)
        {
            if (move == RobotMove.LeftRotate)
            {
                switch (robotHeading)
                {
                    case RobotHeading.North:
                        return RobotHeading.West;
                    case RobotHeading.West:
                        return RobotHeading.South;
                    case RobotHeading.South:
                        return RobotHeading.East;
                    case RobotHeading.East:
                        return RobotHeading.North;
                }
            }
            else if (move == RobotMove.RightRotate)
            {
                switch (robotHeading)
                {
                    case RobotHeading.North:
                        return RobotHeading.East;
                    case RobotHeading.West:
                        return RobotHeading.North;
                    case RobotHeading.South:
                        return RobotHeading.West;
                    case RobotHeading.East:
                        return RobotHeading.South;
                }
            }

            return robotHeading;
        }
    }
}
