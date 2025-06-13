using Robot_Wars.Common;
using Robot_Wars.Implementation;
using Robot_Wars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot_Wars.Common.Enumerations.Enumerations;

namespace Robot_Wars
{
    class Program
    {
        static void Main(string[] args)
        {
            IArena arena = null;
            IRobot robot = null;
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
            };
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    return;

                var @params = input.Split(new char[] { ' ' });
                if (@params.Length == 1 && robot != null)  //executing command on robot  by this time robot and arena created
                {
                    foreach (char item in input)
                    {
                        CommandInstructions ins;
                        if (Enum.TryParse<CommandInstructions>(item.ToString(), out ins))
                        {
                            robot.CommandInstruction(ins);
                        }

                    }
                    Console.WriteLine($"{robot.Position.X} {robot.Position.Y} {robot.RobotDirection}");

                }
                else if (@params.Length == 3 && arena != null) //creating robot loading commands and postioning
                {
                    Dictionary<CommandInstructions, ICommand> commands = new Dictionary<CommandInstructions, ICommand>
                    {
                        {CommandInstructions.L,new RotateLeftCommand(new RotateLogic())
                        },
                        { CommandInstructions.R,new RotateRightCommand(new RotateLogic()) },
                        { CommandInstructions.M,new MoveCommand( new MoveLogic()) }
                    };
                    robot = new Robot(arena, commands);
                    int x;
                    int y;
                    if (int.TryParse(@params[0], out x) && (int.TryParse(@params[1], out y)))
                    {
                        robot.Position = new ArenaPoints(x, y);
                        Direction dir;
                        Enum.TryParse<Direction>(@params[2], out dir);
                        robot.RobotDirection = dir;
                    }
                    else
                    {
                        Console.WriteLine($"{@params[0]}, {@params[1]} must be numbers");
                    }
                }
                else if (@params.Length == 2 && arena == null)  //creating arena
                {
                    int x;
                    int y;
                    if (int.TryParse(@params[0], out x) && (int.TryParse(@params[1], out y)))
                    {
                        arena = new RobotArena(x, y);
                    }
                    else
                    {
                        Console.WriteLine($"{@params[0]}, {@params[1]} must be numbers");
                    }


                }

            }


        }


    }
}
