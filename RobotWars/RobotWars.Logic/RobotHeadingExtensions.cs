using System;

namespace RobotWars.Logic
{
    public static class RobotHeadingExtensions
    {
        public static string ToCodeString(this RobotHeading robotHeading)
        { 
            return new String(new[] { (char) robotHeading });
        }
    }
}
