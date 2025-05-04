using MarcoAerlicRobotWars.Enums;

namespace MarcoAerlicRobotWars
{
    public interface InterfaceWarRobot
    {
        WarRobotDirection Direction { get; }
        uint Latitude { get; }
        uint Longitude { get; }
        InterfaceBattleArena BattleArena { get; set; }
        void Move();
        void Rotate(bool clockwise = true);

        /// <summary>
        /// Enter an arena and go to a specified location 
        /// </summary>
        /// <param name="arena">Arena to enter</param>
        /// <param name="x">X coordinate (latitude)</param>
        /// <param name="y">Y coordinate (longitude)</param>
        /// <param name="direction">direction robot is facing</param>
        /// <returns>True if arena entered successfully, otherwise false</returns>
        bool EnterArena(InterfaceBattleArena arena, uint x, uint y, WarRobotDirection direction);
    }
}