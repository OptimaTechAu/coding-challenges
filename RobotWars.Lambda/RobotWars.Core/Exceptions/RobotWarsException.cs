
namespace RobotWars.Core.Exceptions
{
    public class RobotWarsException : Exception
    {
        public RobotWarsException(string message) : base(message)
        {
        }

        public RobotWarsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
