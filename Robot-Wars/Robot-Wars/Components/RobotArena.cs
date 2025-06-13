using Robot_Wars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Wars.Implementation
{
    public class RobotArena : IArena
    {
        int _width;
        int _length;
        public RobotArena(int length, int width)
        {
            if(length>0 && width>0)
            {
                _length = length;
                _width = width;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }

        public int Length => _length;

        public int Width => _width;
    }
}
