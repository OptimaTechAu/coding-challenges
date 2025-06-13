using System;
using System.Linq;

using Ninject;

using MarcoAerlicRobotWars.Creators;
using MarcoAerlicRobotWars.CommandReaders;
using MarcoAerlicRobotWars.Commands;
using MarcoAerlicRobotWars.Loggers;

namespace MarcoAerlicRobotWars.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            BuildDependencies(kernel);

            var commandReaders = kernel.GetAll<InterfaceCommandReader>().ToList();

            bool exit = false;
            while (!exit)
            {
                var command = Console.ReadLine();
                if (command == "exit" || command == "quit")
                {
                    exit = true;
                    continue;
                }

                foreach (var commandReader in commandReaders)
                {
                    if (!commandReader.Validate(command))
                    {
                        continue;
                    }

                    commandReader.Process(command);
                    break;
                }
            }
        }

        private static void BuildDependencies(IKernel kernel)
        {
            kernel.Bind<InterfaceState>().To<State>().InThreadScope();
            kernel.Bind<InterfaceBattleArenaCreator>().To<BattleArenaCreator>();
            kernel.Bind<InterfaceWarRobotCreator>().To<WarRobotCreator>();
            kernel.Bind<InterfaceLogger>().To<CommandLineLogger>();

            RegisterCommandReaders(kernel);
            RegisterCommands(kernel);
        }

        private static void RegisterCommandReaders(IKernel kernel)
        {
            kernel.Bind<InterfaceCommandReader>().To<CreateArenaCommandReader>();
            kernel.Bind<InterfaceCommandReader>().To<CreateRobotCommandReader>();
            kernel.Bind<InterfaceCommandReader>().To<MoveRobotCommandReader>();
        }

        private static void RegisterCommands(IKernel kernel)
        {
            kernel.Bind<InterfaceCommand>().To<MoveCommand>();
            kernel.Bind<InterfaceCommand>().To<RotateCommand>();
        }
    }
}