namespace RobotWars.Logic.Parsing
{
    public record InputResultArenaInit
    {
        public string ParseErrorMessage { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }        

        internal static InputResultArenaInit Invalid(string message)
        {
            return new InputResultArenaInit { ParseErrorMessage = message };
        }
    }
}
