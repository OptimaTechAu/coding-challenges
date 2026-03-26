using MarcoAerlicRobotWars.Enums;

namespace MarcoAerlicRobotWars
{
    public class WarRobot : InterfaceWarRobot
    {
        public WarRobot()
        {
            this.Direction = WarRobotDirection.North;
        }

        public WarRobotDirection Direction { get; private set; }
        public uint Latitude { get; private set; }
        public uint Longitude { get; private set; }
        public InterfaceBattleArena BattleArena { get; set; }

        /// <summary>
        /// Move robot forward 1 unit
        /// </summary>
        public void Move()
        {
            if (this.BattleArena == null)
            {
                return;
            }

            switch (Direction)
            {
                case WarRobotDirection.North:
                    if (this.Longitude < this.BattleArena.UpperLongitude)
                    {
                        this.Longitude++;
                    }
                    break;
                case WarRobotDirection.East:
                    if (this.Latitude < this.BattleArena.UpperLatitude)
                    {
                        this.Latitude++;
                    }
                    break;
                case WarRobotDirection.South:
                    if (this.Longitude > 0)
                    {
                        this.Longitude--;                        
                    }
                    break;
                case WarRobotDirection.West:
                    if (this.Latitude > 0)
                    {
                        this.Latitude--;
                    }
                    break;
            }
        }

        /// <summary>
        /// Rotate the robot clockwise or anticlockwise
        /// </summary>
        public void Rotate(bool clockwise = true)
        {
            if (clockwise)
            {
                this.Direction = this.Direction == WarRobotDirection.West ? WarRobotDirection.North 
                                                                       : (WarRobotDirection) (((int) this.Direction) << 1);
            }
            else
            {
                this.Direction = this.Direction == WarRobotDirection.North ? WarRobotDirection.West 
                                                                        : (WarRobotDirection) (((int) this.Direction) >> 1);
            }
        }

        /// <summary>
        /// Enter an arena and go to a specified location 
        /// </summary>
        /// <param name="arena">Arena to enter</param>
        /// <param name="x">X coordinate (latitude)</param>
        /// <param name="y">Y coordinate (longitude)</param>
        /// <param name="direction"></param>
        /// <returns>True if arena entered successfully, otherwise false</returns>
        public bool EnterArena(InterfaceBattleArena arena, uint x, uint y, WarRobotDirection direction)
        {
            if (arena == null || x > arena.UpperLatitude || y > arena.UpperLongitude)
            {
                return false;
            }

            this.BattleArena = arena;
            this.Latitude = x;
            this.Longitude = y;
            this.Direction = direction;

            return true;
        }
    }
}