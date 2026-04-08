namespace Minesweeper.Core;

/// <summary>
/// Represents the Minesweeper board grid.
/// Contains tiles and provides helper functions
/// for adjacency calculations.
/// </summary>
public class Board
{
    public int Size { get; }
    public Tile[,] Tiles { get; }

    public Board(int size)
    {
        Size = size;
        Tiles = new Tile[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                Tiles[r, c] = new Tile();
            }
        }
    }

    public bool InBounds(int r, int c)
    {
        return r >= 0 && c >= 0 && r < Size && c < Size;
    }
}