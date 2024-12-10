using Aoc2024Net.Utilities;
using Trail = System.Collections.Generic.List<Aoc2024Net.Utilities.Coordinate>;

namespace Aoc2024Net.Days
{
    internal sealed class Day10 : Day
    {
        private static readonly Coordinate[] Moves =
        {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1),
        };

        public override object? SolvePart1() => GetTrails()
            .GroupBy(t => t.First())
            .Sum(g => g.Select(t => t.Last()).Distinct().Count());

        public override object? SolvePart2() => GetTrails()
            .Count();

        private IEnumerable<Trail> GetTrails()
        {
            var (grid, width, height) = InputData.GetInputInt32Grid(0, ' ');
            var trailHeads = grid
                .GetAllCoordinates()
                .Where(c => grid.At(c) == 0)
                .ToArray();

            foreach (var trailHead in trailHeads)
            {
                var queue = new Queue<Trail>();
                queue.Enqueue([trailHead]);

                while (queue.Count != 0)
                {
                    var trail = queue.Dequeue();
                    if (trail.Count == 10)
                        yield return trail;

                    var nextPoints = Moves
                        .Select(m => trail.Last() + m)
                        .Where(c =>
                            grid.IsInGrid(width, height, c) &&
                            grid.At(c) == grid.At(trail.Last()) + 1)
                        .ToArray();

                    foreach (var point in nextPoints)
                    {
                        queue.Enqueue([.. trail, point]);
                    }
                }
            }
        }
    }
}
