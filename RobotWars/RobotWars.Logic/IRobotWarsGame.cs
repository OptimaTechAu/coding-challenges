namespace RobotWars.Logic
{
    public interface IRobotWarsGame
    {
        int ArenaHeight { get; }
        int ArenaWidth { get; }
        GameStatus GameStatus { get; }

        Robot[] GetRobots();
        InstructionProcssingResult ProcessInstruction(string instruction);
    }
}