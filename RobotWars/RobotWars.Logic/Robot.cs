namespace RobotWars.Logic
{
    public record Robot
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public RobotHeading Heading { get; set; }

        public Robot(int column, int row, RobotHeading heading)
        {
            Column = column;
            Row = row;
            Heading = heading;
        }

        public override string ToString()
        {
            return $"{Column} {Row} {Heading.ToCodeString()}";
        }
    }
}
