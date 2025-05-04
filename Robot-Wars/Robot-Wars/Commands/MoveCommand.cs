using Robot_Wars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Wars.Implementation
{
    public class MoveCommand : ICommand
    {
        private readonly MoveLogic _logic;

        public MoveCommand(MoveLogic logic)
        {
            _logic = logic;
        }
        public void ExecuteCommand(IRobot robot)
        {
            robot.Position = _logic.Move(robot.RobotDirection, robot.Position);
        }
    }
}
