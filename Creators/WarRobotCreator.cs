namespace MarcoAerlicRobotWars.Creators
{
    public class WarRobotCreator : InterfaceWarRobotCreator
    {
        public InterfaceWarRobot Create()
        {
            return new WarRobot();
        }
    }
}