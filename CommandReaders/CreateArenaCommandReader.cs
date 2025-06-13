using System;
using System.Text.RegularExpressions;
using MarcoAerlicRobotWars.Creators;
using MarcoAerlicRobotWars.Loggers;

namespace MarcoAerlicRobotWars.CommandReaders
{
    public class CreateArenaCommandReader : CommandReader
    {
        private const string latitudeGroupName = "Latitude";
        private const string longitudeGroupName = "Longitude";
        private static readonly string regexPattern = String.Format(@"^(?<{0}>\d+) (?<{1}>\d+)$", latitudeGroupName, longitudeGroupName);

        public CreateArenaCommandReader(InterfaceState state, InterfaceLogger logger)
            : base(regexPattern, state, logger)
        {
        }

        public override void Process(string command)
        {
            Match match;
            if (!this.Validate(command, out match))
            {
                return;
            }

            uint latitude = Convert.ToUInt32(match.Groups[latitudeGroupName].Value);
            uint longitude = Convert.ToUInt32(match.Groups[longitudeGroupName].Value);
            this.state.BattleArena = this.state.BattleArenaCreator.Create(latitude, longitude);

            this.logger.Log(this.state.BattleArena != null ? "Arena creation successful" 
                                                       : "Arena creation failed");
        }
    }
}