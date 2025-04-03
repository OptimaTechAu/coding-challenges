namespace RobotWarServerless.Services
{
    public interface IRobotCommandProcessor
    {
        IEnumerable<string> Process(string input);
    }
}