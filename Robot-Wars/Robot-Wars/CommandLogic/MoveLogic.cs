using Robot_Wars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars.Implementation
{
    public class MoveLogic
    {
        public ArenaPoints Move(Direction compassDirection, ArenaPoints position)
        {
            switch (compassDirection)
            {
                case Direction.N:
                    return new ArenaPoints(position.X, position.Y + 1);

                case Direction.S:
                    return new ArenaPoints(position.X, position.Y - 1);
                case Direction.W:
                    return new ArenaPoints(position.X - 1, position.Y);
                case Direction.E:
                    return new ArenaPoints(position.X + 1, position.Y);
                default:
                    break;
            }
            return position;

        }
    }
}
