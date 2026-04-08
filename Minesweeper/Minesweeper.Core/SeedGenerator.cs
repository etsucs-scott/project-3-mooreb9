namespace Minesweeper.Core;

// Generates a seed using current time
// if the user does not provide one
public static class SeedGenerator
{
    public static int Generate()
    {
        return (int)DateTime.Now.Ticks % int.MaxValue;
    }
}
