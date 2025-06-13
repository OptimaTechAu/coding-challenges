using Robot_Wars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars.Interfaces
{
    public interface IRobot
    {
        ArenaPoints Position { get; set; }
        Direction RobotDirection { get; set; }
        void CommandInstruction(CommandInstructions instructions);

    }
   
    
}
