using System.Collections.Generic;
using System.Linq;
using MarcoAerlicRobotWars.Commands;
using MarcoAerlicRobotWars.Loggers;

namespace MarcoAerlicRobotWars.CommandReaders
{
    public class MoveRobotCommandReader : CommandReader
    {
        private readonly IList<InterfaceCommand> commands;

        public MoveRobotCommandReader(InterfaceState state, IEnumerable<InterfaceCommand> commands, InterfaceLogger logger )
            : base("^[m|r|l]+$", state, logger)
        {
            this.commands = commands.ToList();
        }

        public override void Process(string command)
        {
            if (!this.Validate(command))
            {
                return;
            }

            InterfaceWarRobot robot = this.state.LatestRobot;
            foreach (var character in command.ToLowerInvariant())
            {
                InterfaceCommand executer = GetExecuter(character);
                executer.Execute(character, robot);
            }

            this.logger.Log("Robot movement completed");
            this.logger.Log(string.Format("Robot Position: {0} {1} {2}", robot.Latitude, robot.Longitude, robot.Direction));
        }

        private InterfaceCommand GetExecuter(char character)
        {
            return this.commands.FirstOrDefault(command => command.IsValid(character));
        }
    }
}