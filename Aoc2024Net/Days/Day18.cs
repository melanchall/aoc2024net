using Aoc2024Net.Utilities;

namespace Aoc2024Net.Days
{
    internal sealed class Day18 : Day
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
            var (grid, size, bytesCoordinates, count) = GetInputData();

            foreach (var c in bytesCoordinates.Take(count))
            {
                grid.SetAt(c, true);
            }

            return GetPathLength(grid, size);
        }

        public override object? SolvePart2()
        {
            var (grid, size, bytesCoordinates, _) = GetInputData();

            foreach (var c in bytesCoordinates)
            {
                grid.SetAt(c, true);

                if (GetPathLength(grid, size) == null)
                    return $"{c.X},{c.Y}";
            }

            return null;
        }

        private (bool[,] Grid, int Size, Coordinate[] BytesCoordinates, int Count) GetInputData()
        {
            var size = (int)Parameters[0];
            var count = (int)Parameters[1];
            var grid = new bool[size, size];
            var bytesCoordinates = InputData
                .GetInputLines()
                .Select(l => l.Split(',').Select(int.Parse).ToArray())
                .Select(xy => new Coordinate(xy[0], xy[1]))
                .ToArray();

            return (grid, size, bytesCoordinates, count);
        }

        private static int? GetPathLength(bool[,] grid, int size)
        {
            var start = new Coordinate(0, 0);
            var end = new Coordinate(size - 1, size - 1);

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
                    !grid.IsInGrid(size, size, currentPathEnd) ||
                    grid.At(currentPathEnd) == true)
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
