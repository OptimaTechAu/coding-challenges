using MarcoAerlicRobotWars.Creators;

namespace MarcoAerlicRobotWars
{
    public interface InterfaceState
    {
        InterfaceBattleArena BattleArena { get; set; }
        InterfaceWarRobot LatestRobot { get; set; }

        InterfaceBattleArenaCreator BattleArenaCreator { get; }
        InterfaceWarRobotCreator WarRobotCreator { get; }
    }
}