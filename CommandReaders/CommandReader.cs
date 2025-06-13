using System.Text.RegularExpressions;
using MarcoAerlicRobotWars.Loggers;

namespace MarcoAerlicRobotWars.CommandReaders
{
    public abstract class CommandReader : InterfaceCommandReader
    {
        protected readonly InterfaceState state;
        protected readonly InterfaceLogger logger;
        private readonly Regex regex;

        /// <summary>
        /// Initialise a command reader with a regular 
        /// expression pattern that defines a valid command
        /// </summary>
        protected CommandReader(string pattern, InterfaceState state, InterfaceLogger logger)
        {
            this.state = state;
            this.logger = logger;
            this.regex = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public bool Validate(string command)
        {
            return this.regex.IsMatch(command);
        }

        public bool Validate(string command, out Match match)
        {
            match = this.regex.Match(command);
            return match.Success;
        }

        public abstract void Process(string command);
    }
}