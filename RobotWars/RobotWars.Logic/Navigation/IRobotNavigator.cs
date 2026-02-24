namespace RobotWars.Logic.Navigation
{
    public interface IRobotNavigator
    {
        PlanMoveResult PlanMove(Robot robot, RobotMove move);
        Robot Move(Robot robot, RobotMove move);
        RobotHeading Rotate(RobotHeading robotHeading, RobotMove move);
    }
}
