namespace Minesweeper.Core;

/// <summary>
/// Handles loading and saving high scores from CSV file.
/// </summary>
public class HighScoreManager
{
    private const string PathFile = "data/highscores.csv";

    public List<HighScore> Load()
    {
        var scores = new List<HighScore>();

        try
        {
            if (!File.Exists(PathFile))
            {
                Directory.CreateDirectory("data");
                File.WriteAllText(PathFile,
                    "size,seconds,moves,seed,timestamp\n");
                return scores;
            }

            var lines = File.ReadAllLines(PathFile).Skip(1);

            foreach (var line in lines)
            {
                var p = line.Split(',');

                scores.Add(new HighScore
                {
                    Size = int.Parse(p[0]),
                    Seconds = int.Parse(p[1]),
                    Moves = int.Parse(p[2]),
                    Seed = int.Parse(p[3]),
                    Timestamp = DateTime.Parse(p[4])
                });
            }
        }
        catch
        {
            Console.WriteLine("Error reading high scores.");
        }

        return scores;
    }

    public void Save(List<HighScore> scores)
    {
        var lines = new List<string>
        {
            "size,seconds,moves,seed,timestamp"
        };

        lines.AddRange(scores.Select(s =>
            $"{s.Size},{s.Seconds},{s.Moves},{s.Seed},{s.Timestamp}"));

        File.WriteAllLines(PathFile, lines);
    }
}