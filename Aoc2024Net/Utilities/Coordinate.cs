namespace Aoc2024Net.Utilities
{
    public record Coordinate(int X, int Y)
    {
        public static Coordinate operator +(Coordinate a, Coordinate b) =>
            new Coordinate(a.X + b.X, a.Y + b.Y);
    }
}
