using Robot_Wars.Common;
using Robot_Wars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars.Implementation
{
    public class Robot : IRobot
    {

        private readonly IArena _warArena;
        private Dictionary<CommandInstructions, ICommand> _commands;
        public Robot(IArena warArena, Dictionary<CommandInstructions, ICommand> commands)
        {
            _commands = commands;
            _warArena = warArena;
            Position = new ArenaPoints(_warArena.Length, _warArena.Width);
            RobotDirection = Direction.S;
        }
        public ArenaPoints Position { get; set; }
        public Direction RobotDirection { get; set; }

        public void CommandInstruction(CommandInstructions instructions)
        {
            if (_commands.ContainsKey(instructions))
            {
                _commands[instructions].ExecuteCommand(this);
            }
        }
    }
}
