using Aoc2024Net.Utilities;

namespace Aoc2024Net.Days
{
    internal sealed class Day6 : Day
    {
        private const char GuardSymbol = '^';
        private const char QbstructionSymbol = '#';
        private const char EmptySpaceSymbol = '.';

        private static readonly Dictionary<Coordinate, Coordinate> Turns = new()
        {
            [new Coordinate(0, 1)] = new Coordinate(-1, 0),
            [new Coordinate(0, -1)] = new Coordinate(1, 0),
            [new Coordinate(1, 0)] = new Coordinate(0, 1),
            [new Coordinate(-1, 0)] = new Coordinate(0, -1),
        };

        public override object? SolvePart1()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var coordinates = GridUtilities.GetAllCoordinates(grid);

            var direction = new Coordinate(0, -1);
            var position = coordinates.First(c => grid.At(c) == GuardSymbol);
            var visitedPositions = new HashSet<Coordinate> { position };

            while (true)
            {
                var nextPosition = position + direction;
                if (!grid.IsInGrid(width, height, nextPosition))
                    break;

                if (grid.At(nextPosition) == QbstructionSymbol)
                {
                    direction = Turns[direction];
                    continue;
                }

                visitedPositions.Add(nextPosition);
                position = nextPosition;
            }

            return visitedPositions.Count;
        }

        public override object? SolvePart2()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var coordinates = GridUtilities.GetAllCoordinates(grid);

            var startDirection = new Coordinate(0, -1);
            var startPosition = coordinates.First(c => grid.At(c) == GuardSymbol);
            
            var result = 0;

            foreach (var coordinate in coordinates)
            {
                if (grid.At(coordinate) != EmptySpaceSymbol)
                    continue;

                grid.SetAt(coordinate, QbstructionSymbol);

                var direction = startDirection;
                var position = startPosition;
                var visitedVectors = new HashSet<Vector> { new (position, direction) };

                while (true)
                {
                    var nextPosition = position + direction;
                    if (!grid.IsInGrid(width, height, nextPosition))
                        break;

                    if (grid.At(nextPosition) == QbstructionSymbol)
                    {
                        direction = Turns[direction];
                        continue;
                    }

                    if (!visitedVectors.Add(new (nextPosition, direction)))
                    {
                        result++;
                        break;
                    }

                    position = nextPosition;
                }

                grid.SetAt(coordinate, EmptySpaceSymbol);
            }

            return result;
        }
    }
}
