using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Wars.Interfaces
{
    public interface ICommand
    {
        void ExecuteCommand(IRobot robot);
    }
}
