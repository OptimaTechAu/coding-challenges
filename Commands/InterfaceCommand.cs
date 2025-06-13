namespace MarcoAerlicRobotWars.Commands
{
    public interface InterfaceCommand
    {
        bool Execute(char command, InterfaceWarRobot robot);
        bool IsValid(char command);
    }
}