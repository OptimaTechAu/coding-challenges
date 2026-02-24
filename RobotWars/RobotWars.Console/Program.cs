using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotWars.Logic;
using RobotWars.Logic.Navigation;
using RobotWars.Logic.Parsing;

namespace RobotWars.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var inputParser = host.Services.GetService<IInputParser>();

            var game = host.Services.GetService<IRobotWarsGame>();

            while (true)
            {
                System.Console.Write(">");
                string input = System.Console.ReadLine();

                // Check if quitting
                if (inputParser.IsQuit(input))
                {
                    return;
                }

                // Process input
                var result = game.ProcessInstruction(input);
                if (!result.Successful)
                {
                    System.Console.WriteLine(result.FailureMessage);
                }

                // Output robot state
                var robots = game.GetRobots();
                if (robots == null || robots.Length == 0)
                {
                    System.Console.WriteLine("[EMPTY]");
                }
                else
                {
                    foreach (var robot in robots)
                    {
                        System.Console.WriteLine(robot.ToString());
                    }
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection svc)
        {
            svc.AddSingleton<IInputParser, InputParser>();
            svc.AddSingleton<IRobotWarsGame, RobotWarsGame>();
            svc.AddSingleton<IRobotNavigator, RobotNavigator>();
        }
    }
}
