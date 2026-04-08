namespace Minesweeper.Core;

/// <summary>
/// Generates deterministic mine placement using a seed
/// and computes adjacent mine counts.
/// </summary>
public static class BoardGenerator
{
    public static void Generate(Board board, int mineCount, int seed)
    {
        Random rand = new Random(seed);
        int placed = 0;

        while (placed < mineCount)
        {
            int r = rand.Next(board.Size);
            int c = rand.Next(board.Size);

            if (!board.Tiles[r, c].HasMine)
            {
                board.Tiles[r, c].HasMine = true;
                placed++;
            }
        }

        ComputeAdjacency(board);
    }

    private static void ComputeAdjacency(Board board)
    {
        for (int r = 0; r < board.Size; r++)
        {
            for (int c = 0; c < board.Size; c++)
            {
                int count = 0;

                for (int dr = -1; dr <= 1; dr++)
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        int nr = r + dr;
                        int nc = c + dc;

                        if (board.InBounds(nr, nc) && board.Tiles[nr, nc].HasMine)
                            count++;
                    }

                if (board.Tiles[r, c].HasMine)
                    count--;

                board.Tiles[r, c].AdjacentMines = count;
            }
        }
    }
}