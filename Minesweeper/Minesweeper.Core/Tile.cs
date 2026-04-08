namespace Minesweeper.Core;

/// <summary>
/// Represents a single tile on the board.
/// Stores mine presence, reveal state, flag state,
/// and number of adjacent mines.
/// </summary>
public class Tile
{
    public bool HasMine { get; set; }
    public bool IsRevealed { get; set; }
    public bool IsFlagged { get; set; }
    public int AdjacentMines { get; set; }
}