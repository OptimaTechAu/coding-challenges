using MarcoAerlicRobotWars.Creators;

namespace MarcoAerlicRobotWars
{
    public class State : InterfaceState
    {
        public State(InterfaceBattleArenaCreator battleArenaCreator, InterfaceWarRobotCreator robotCreator)
        {
            BattleArenaCreator = battleArenaCreator;
            WarRobotCreator = robotCreator;
        }

        public InterfaceBattleArenaCreator BattleArenaCreator { get; private set; }
        public  InterfaceWarRobotCreator WarRobotCreator { get; private set; }

        public InterfaceBattleArena BattleArena { get; set; }
        public InterfaceWarRobot LatestRobot { get; set; }
    }   
}