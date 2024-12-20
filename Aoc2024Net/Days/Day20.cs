using Aoc2024Net.Utilities;
using System.Diagnostics;

namespace Aoc2024Net.Days
{
    internal sealed class Day20 : Day
    {
        private record PathCoordinate(Coordinate Coordinate, int PathLength);

        private static readonly Coordinate[] Moves =
        [
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0),
        ];

        public override object? SolvePart1()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var ccc = grid.GetAllCoordinates();
            var start = ccc.First(c => grid.At(c) == 'S');
            var end = ccc.First(c => grid.At(c) == 'E');

            var bestPath = GetPath(grid, width, height, start, end);
            var bestPathLength = bestPath.Count - 1;

            var possibleCheatPositions = bestPath
                .SelectMany(p => Moves.Select(m => p + m))
                .Where(p => grid.At(p) == '#')
                .Distinct()
                .ToArray();

            var dict = new Dictionary<int, int>();
            var i = 0;

            foreach (var c in possibleCheatPositions)
            {
                i++;
                Debug.WriteLine($"{i} / {possibleCheatPositions.Length}");

                grid.SetAt(c, '.');

                var p = GetPathLength(grid, width, height, start, end, bestPathLength);
                if (p != null)
                {
                    var diff = bestPathLength - p.Value;
                    if (diff > 0)
                    {
                        if (!dict.ContainsKey(diff))
                            dict[diff] = 0;

                        dict[diff]++;
                    }
                }

                grid.SetAt(c, '#');
            }

            return dict.Where(kv => kv.Key >= 100).Sum(kv => kv.Value);
        }

        public override object? SolvePart2() => null;

        private static List<Coordinate> GetPath(
            char[,] grid,
            int width,
            int height,
            Coordinate start,
            Coordinate end)
        {
            var visited = new HashSet<Coordinate>();
            var queue = new PriorityQueue<List<Coordinate>, int>();

            void EnqueuePath(List<Coordinate> path) =>
                queue.Enqueue(path, path.Count);

            EnqueuePath(new List<Coordinate> { start });

            while (queue.Count > 0)
            {
                var currentPathCoordinate = queue.Dequeue();
                var currentPathEnd = currentPathCoordinate.Last();

                if (!visited.Add(currentPathEnd) ||
                    !grid.IsInGrid(width, height, currentPathEnd) ||
                    grid.At(currentPathEnd) == '#')
                    continue;

                if (currentPathEnd == end)
                    return currentPathCoordinate;

                foreach (var move in Moves)
                {
                    EnqueuePath([.. currentPathCoordinate, currentPathEnd + move]);
                }
            }

            return null;
        }

        private static int? GetPathLength(
            char[,] grid,
            int width,
            int height,
            Coordinate start,
            Coordinate end,
            int threshold)
        {
            var visited = new HashSet<Coordinate>();
            var queue = new PriorityQueue<PathCoordinate, int>();

            void EnqueuePath(PathCoordinate path) =>
                queue.Enqueue(path, path.PathLength);

            EnqueuePath(new PathCoordinate(start, 0));

            while (queue.Count > 0)
            {
                var currentPathCoordinate = queue.Dequeue();
                var currentPathEnd = currentPathCoordinate.Coordinate;

                if (!visited.Add(currentPathEnd) ||
                    !grid.IsInGrid(width, height, currentPathEnd) ||
                    grid.At(currentPathEnd) == '#' ||
                    currentPathCoordinate.PathLength >= threshold)
                    continue;

                if (currentPathEnd == end)
                    return currentPathCoordinate.PathLength;

                foreach (var move in Moves)
                {
                    EnqueuePath(new PathCoordinate(
                        currentPathEnd + move,
                        currentPathCoordinate.PathLength + 1));
                }
            }

            return null;
        }
    }
}
