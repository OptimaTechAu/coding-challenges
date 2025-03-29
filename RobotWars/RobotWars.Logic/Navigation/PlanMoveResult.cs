namespace RobotWars.Logic.Navigation
{
    public record PlanMoveResult
    {
        public bool MovedPosition { get; set; }
        public Robot Robot { get; set; }

        public static PlanMoveResult RotateResult(Robot robot)
        {
            return new PlanMoveResult { MovedPosition = false, Robot = robot };
        }

        internal static PlanMoveResult MoveResult(Robot robot)
        {
            return new PlanMoveResult { MovedPosition = true, Robot = robot };
        }
    }
}
