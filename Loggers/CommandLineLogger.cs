using System;

namespace MarcoAerlicRobotWars.Loggers
{
    public class CommandLineLogger : InterfaceLogger
    {
        public void Log(string message)
        {
            Console.WriteLine("LOG: {0}", message);
        }
    }
}