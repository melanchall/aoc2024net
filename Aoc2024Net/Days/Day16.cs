using Aoc2024Net.Utilities;

namespace Aoc2024Net.Days
{
    internal sealed class Day16 : Day
    {
        private record Path(ICollection<Coordinate> Coordinates, Coordinate Direction, long Score);

        private static readonly Dictionary<Coordinate, Coordinate> RightTurns = new()
        {
            [new(0, 1)] = new(-1, 0),
            [new(0, -1)] = new(1, 0),
            [new(1, 0)] = new(0, 1),
            [new(-1, 0)] = new(0, -1),
        };

        private static readonly Dictionary<Coordinate, Coordinate> LeftTurns = new()
        {
            [new(0, 1)] = new(1, 0),
            [new(0, -1)] = new(-1, 0),
            [new(1, 0)] = new(0, -1),
            [new(-1, 0)] = new(0, 1),
        };

        public override object? SolvePart1() =>
            GetBestPaths().First().Score;

        public override object? SolvePart2() =>
            GetBestPaths().SelectMany(p => p.Coordinates).Distinct().Count();

        private ICollection<Path> GetBestPaths()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();

            var allCoordinates = grid.GetAllCoordinates();
            var start = allCoordinates.First(c => grid.At(c) == 'S');
            var end = allCoordinates.First(c => grid.At(c) == 'E');

            var visitedCounts = new Dictionary<Coordinate, int>();
            var maxPassesThroughSameTile = 20;

            var queue = new PriorityQueue<Path, long>();

            void EnqueuePath(Path path) =>
                queue.Enqueue(path, path.Score);

            EnqueuePath(new Path([start], new(1, 0), 0));

            var bestPaths = new List<Path>();
            var bestScore = long.MaxValue;

            while (queue.Count > 0)
            {
                var currentPath = queue.Dequeue();
                var currentPathEnd = currentPath.Coordinates.Last();

                if ((visitedCounts.TryGetValue(currentPathEnd, out var count) && count > maxPassesThroughSameTile) ||
                    grid.At(currentPathEnd) == '#' ||
                    currentPath.Score > bestScore)
                    continue;

                if (currentPathEnd == end)
                {
                    bestPaths.Add(currentPath);
                    bestScore = currentPath.Score;
                    continue;
                }

                if (!visitedCounts.ContainsKey(currentPathEnd))
                    visitedCounts[currentPathEnd] = 0;

                visitedCounts[currentPathEnd]++;

                var nextPaths = new[]
                {
                    new Path(
                        [..currentPath.Coordinates, currentPathEnd + currentPath.Direction],
                        currentPath.Direction,
                        currentPath.Score + 1),
                    new Path(
                        [.. currentPath.Coordinates],
                        LeftTurns[currentPath.Direction],
                        currentPath.Score + 1000),
                    new Path(
                        [.. currentPath.Coordinates],
                        RightTurns[currentPath.Direction],
                        currentPath.Score + 1000),
                };

                foreach (var path in nextPaths)
                {
                    EnqueuePath(path);
                }
            }

            return bestPaths;
        }
    }
}
