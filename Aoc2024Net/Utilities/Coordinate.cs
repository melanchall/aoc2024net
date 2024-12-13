namespace Aoc2024Net.Utilities
{
    public record Coordinate(long X, long Y)
    {
        public static Coordinate operator +(Coordinate a, Coordinate b) =>
            new Coordinate(a.X + b.X, a.Y + b.Y);

        public static Coordinate operator *(Coordinate a, long factor) =>
            new Coordinate(a.X * factor, a.Y * factor);
    }
}
