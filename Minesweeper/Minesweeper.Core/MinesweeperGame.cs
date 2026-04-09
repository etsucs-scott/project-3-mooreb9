namespace Minesweeper.Core;

/// <summary>
/// Main game controller class.
/// Handles player actions, win/loss state,
/// and board management.
/// </summary>
public class MinesweeperGame
{
    public Board Board { get; }
    public bool GameOver { get; private set; }
    public bool Win { get; private set; }

    public int Moves { get; private set; }

    public MinesweeperGame(int size, int mines, int seed)
    {
        Board = new Board(size);
        BoardGenerator.Generate(Board, mines, seed);
    }

    public void ToggleFlag(int r, int c)
    {
        var tile = Board.Tiles[r, c];

        if (!tile.IsRevealed)
            tile.IsFlagged = !tile.IsFlagged;

        if (GameOver)
            return;
    }

    public void Reveal(int r, int c)
    {
        var tile = Board.Tiles[r, c];

        if (tile.IsFlagged)
            return;

        Moves++;

        if (tile.HasMine)
        {
            GameOver = true;
            return;
        }

        RevealService.Reveal(Board, r, c);

        CheckWin();

        if (GameOver)
            return;
    }

    private void CheckWin()
    {
        foreach (var tile in Board.Tiles)
        {
            if (!tile.HasMine && !tile.IsRevealed)
                return;
        }

        Win = true;
        GameOver = true;
    }
}
