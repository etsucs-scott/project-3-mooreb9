namespace Minesweeper.Core;

/// <summary>
/// Handles reveal logic and cascade behavior
/// for zero-adjacent tiles.
/// </summary>
public static class RevealService
{
    public static void Reveal(Board board, int r, int c)
    {
        if (!board.InBounds(r, c))
            return;

        var tile = board.Tiles[r, c];

        if (tile.IsRevealed || tile.IsFlagged)
            return;

        tile.IsRevealed = true;

        if (tile.AdjacentMines == 0 && !tile.HasMine)
        {
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                {
                    Reveal(board, r + dr, c + dc);
                }
        }
    }
}