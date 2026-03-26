using Robot_Wars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars.Implementation
{
    public class RotateRightCommand : Interfaces.ICommand
    {
        private readonly RotateLogic _logic;

        public RotateRightCommand(RotateLogic logic)
        {
            _logic = logic;
        }
        public void ExecuteCommand(IRobot robot)
        {
            robot.RobotDirection = _logic.Rotate(CommandInstructions.R, robot.RobotDirection);
        }
    }
}
