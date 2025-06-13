using System;
using System.Text.RegularExpressions;
using MarcoAerlicRobotWars.Enums;
using MarcoAerlicRobotWars.Loggers;

namespace MarcoAerlicRobotWars.CommandReaders
{
    public class CreateRobotCommandReader : CommandReader
    {
        private const string latitudeGroupName = "Latitude";
        private const string longitudeGroupName = "Longitude";
        private const string directionGroupName = "Direction";

        private static readonly string regexPattern = string.Format(@"^(?<{0}>\d+) (?<{1}>\d+) (?<{2}>[n|e|s|w])$", latitudeGroupName, longitudeGroupName, directionGroupName);

        public CreateRobotCommandReader(InterfaceState state, InterfaceLogger logger)
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

            this.state.LatestRobot = this.state.WarRobotCreator.Create();

            if (this.state.LatestRobot == null)
            {
                return;
            }

            uint latitude = Convert.ToUInt32(match.Groups[latitudeGroupName].Value);
            uint longitude = Convert.ToUInt32(match.Groups[longitudeGroupName].Value);
            WarRobotDirection direction = ConvertToDirection(match.Groups[directionGroupName].Value);

            bool robotCreated = this.state.LatestRobot.EnterArena(this.state.BattleArena, latitude, longitude, direction);

            string logMessage = String.Format("Robot creation {0}", robotCreated ? "successful" : "failed");
            this.logger.Log(logMessage);
        }

        private WarRobotDirection ConvertToDirection(string value)
        {
            switch (value.ToLowerInvariant())
            {
                case "e":
                    return WarRobotDirection.East;
                case "s":
                    return WarRobotDirection.South;
                case "w":
                    return WarRobotDirection.West;
                default:
                    return WarRobotDirection.North;
            }
        }
    }
}