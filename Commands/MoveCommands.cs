namespace MarcoAerlicRobotWars.Commands
{
    public class MoveCommand : InterfaceCommand
    {
        public bool Execute(char command, InterfaceWarRobot robot)
        {
            if (robot == null || !this.IsValid(command))
            {
                return false;
            }

            robot.Move();
            return true;
        }

        public bool IsValid(char command)
        {
            return command == 'M' || command == 'm';
        }
    }
}