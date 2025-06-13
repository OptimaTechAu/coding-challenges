using System.Text.RegularExpressions;

namespace MarcoAerlicRobotWars.CommandReaders
{
    public interface InterfaceCommandReader
    {
        bool Validate(string command);
        bool Validate(string command, out Match match);
        void Process(string command);
    }
}