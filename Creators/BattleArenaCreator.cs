namespace MarcoAerlicRobotWars.Creators
{
    public class BattleArenaCreator : InterfaceBattleArenaCreator
    {
        public InterfaceBattleArena Create(uint latitude, uint longitude)
        {
            return new BattleArena(latitude, longitude);
        }
    }
}