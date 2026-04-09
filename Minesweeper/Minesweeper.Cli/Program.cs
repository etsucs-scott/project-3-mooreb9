using Minesweeper.Core;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1) 8x8");
            Console.WriteLine("2) 12x12");
            Console.WriteLine("3) 16x16");
            Console.WriteLine("q) Quit");

            var input = Console.ReadLine()?.Trim().ToLower();
            if (input == "q") break;

            int size = input switch
            {
                "1" => 8,
                "2" => 12,
                "3" => 16,
                _ => 8
            };

            int mines = size switch
            {
                8 => 10,
                12 => 25,
                16 => 40,
            };

            Console.Write("Seed (blank = time): ");
            var seedInput = Console.ReadLine();
            int seed = string.IsNullOrWhiteSpace(seedInput)
                ? SeedGenerator.Generate()
                : int.TryParse(seedInput, out int s) ? s : SeedGenerator.Generate();

            Console.WriteLine($"Seed used: {seed}\n");
            Console.WriteLine("Commands: r row col | f row col | q\n");

            var game = new MinesweeperGame(size, mines, seed);
            PlayGame(game);

            Console.WriteLine("Press Enter to return to menu...");
            Console.ReadLine();
        }
    }

    static void PlayGame(MinesweeperGame game)
    {
        while (!game.GameOver)
        {
            DrawBoard(game);

            Console.Write("> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var cmd = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var action = cmd[0].ToLower();

            if (action == "q") return;

            if (cmd.Length < 3
                || !int.TryParse(cmd[1], out int r)
                || !int.TryParse(cmd[2], out int c)
                || !game.Board.InBounds(r, c))
            {
                Console.WriteLine("Invalid command or coordinates.");
                continue;
            }

            switch (action)
            {
                case "r":
                    if (game.Board.Tiles[r, c].IsFlagged)
                        Console.WriteLine("Tile is flagged. Unflag first.");
                    else
                        game.Reveal(r, c);
                    break;

                case "f":
                    game.ToggleFlag(r, c);
                    break;

                default:
                    Console.WriteLine("Unknown command. Use r, f, or q.");
                    break;
            }
        }

        DrawBoard(game);

        Console.WriteLine(game.Win ? "You win!" : "Boom! Game over.");
    }

    static void DrawBoard(MinesweeperGame game)
    {
        var board = game.Board;

        // Draw column indices
        Console.Write("   ");
        for (int c = 0; c < board.Size; c++)
            Console.Write($"{c,2}");
        Console.WriteLine();

        for (int r = 0; r < board.Size; r++)
        {
            Console.Write($"{r,2} "); // Row index

            for (int c = 0; c < board.Size; c++)
            {
                var t = board.Tiles[r, c];
                char ch = '#';

                if (t.IsFlagged)
                    ch = 'f';
                else if (!t.IsRevealed)
                    ch = '#';
                else if (t.HasMine)
                    ch = 'b';
                else if (t.AdjacentMines == 0)
                    ch = '.';
                else
                    ch = t.AdjacentMines.ToString()[0];

                Console.Write(ch + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}