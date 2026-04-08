namespace Minesweeper.Core;

/// <summary>
/// Represents one high score entry.
/// </summary>
public class HighScore
{
    public int Size { get; set; }
    public int Seconds { get; set; }
    public int Moves { get; set; }
    public int Seed { get; set; }
    public DateTime Timestamp { get; set; }
}