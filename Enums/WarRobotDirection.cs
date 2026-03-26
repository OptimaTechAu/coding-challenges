using System;

namespace MarcoAerlicRobotWars.Enums
{
    [Flags]
    public enum WarRobotDirection
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8
    }
}