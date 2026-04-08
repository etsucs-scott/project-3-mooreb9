using Minesweeper.Core;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Minesweeper");
            Console.WriteLine("1 - 8x8");
            Console.WriteLine("2 - 12x12");
            Console.WriteLine("3 - 16x16");
            Console.WriteLine("q - quit");

            var input = Console.ReadLine();

            if (input == "q")
                break;

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
                16 => 40
            };

            Console.Write("Seed (blank for random): ");
            var seedInput = Console.ReadLine();

            int seed = string.IsNullOrWhiteSpace(seedInput)
                ? SeedGenerator.Generate()
                : int.Parse(seedInput);

            Console.WriteLine($"Seed used: {seed}");

            var game = new MinesweeperGame(size, mines, seed);

            PlayGame(game);
        }
    }

    static void PlayGame(MinesweeperGame game)
    {
        while (!game.GameOver)
        {
            DrawBoard(game);

            Console.Write("> ");
            var cmd = Console.ReadLine().Split(' ');

            if (cmd[0] == "q")
                return;

            int r = int.Parse(cmd[1]);
            int c = int.Parse(cmd[2]);

            if (cmd[0] == "r")
                game.Reveal(r, c);

            if (cmd[0] == "f")
                game.ToggleFlag(r, c);
        }

        DrawBoard(game);

        Console.WriteLine(game.Win ? "You win!" : "Boom! Game over.");
    }

    static void DrawBoard(MinesweeperGame game)
    {
        var board = game.Board;

        for (int r = 0; r < board.Size; r++)
        {
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
    }
}