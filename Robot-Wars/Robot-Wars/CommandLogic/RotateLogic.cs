using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars.Implementation
{
    public class RotateLogic
    {
        Dictionary<Direction, Direction> right;
        Dictionary<Direction, Direction> left;
        public RotateLogic()
        {
            right = new Dictionary<Direction, Direction>()
            {
                {Direction.N,Direction.E },
                {Direction.S,Direction.W },
                {Direction.E,Direction.S },
                {Direction.W,Direction.N }

            };
            left = new Dictionary<Direction, Direction>()
            {
                 {Direction.N,Direction.W },
                 {Direction.S,Direction.E },
                 {Direction.E,Direction.N },
                 {Direction.W,Direction.S }
            };

        }
        public Direction Rotate(CommandInstructions rotateDirection, Direction compassDirection)
        {
            switch (rotateDirection)
            {
                case CommandInstructions.L:
                    return left[compassDirection];

                case CommandInstructions.R:
                    return right[compassDirection];
                default:
                    break;
            }
            return compassDirection;
        }

    }
}
