namespace RobotWars.Logic.Parsing
{
    public class InputResultRobotMove
    {
        public string ParseErrorMessage { get; set; }
        public RobotMove[] RobotMoves { get; set; }

        internal static InputResultRobotMove Invalid(string message)
        {
            return new InputResultRobotMove { ParseErrorMessage = message };
        }
    }
}