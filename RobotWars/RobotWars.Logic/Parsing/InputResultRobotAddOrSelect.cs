namespace RobotWars.Logic.Parsing
{
    public record InputResultRobotAddOrSelect
    {
        public string ParseErrorMessage { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public RobotHeading RobotHeading { get; set; }

        internal static InputResultRobotAddOrSelect Invalid(string message)
        {
            return new InputResultRobotAddOrSelect { ParseErrorMessage = message };
        }
    }
}
