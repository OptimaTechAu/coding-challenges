using RobotWars.Core.Interfaces;
using RobotWars.Core.Services;

namespace RobotWars.Lambda.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRobotWarsServices(this IServiceCollection services)
        {
            services.AddSingleton<IRobotService, RobotService>();
            services.AddSingleton<IArenaService, ArenaService>();
            services.AddSingleton<ICommandProcessor, CommandProcessor>();

            return services;
        }
    }
}