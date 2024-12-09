using Aoc2024Net.Utilities;

namespace Aoc2024Net.Days
{
    internal sealed class Day8 : Day
    {
        public override object? SolvePart1() =>
            Solve(false);

        public override object? SolvePart2() =>
            Solve(true);

        public int Solve(bool moreAntinodes)
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var coords = grid.GetAllCoordinates();

            var frequenciesGroups = coords
                .GroupBy(grid.At)
                .Where(g => g.Key != '.')
                .Select(g => g.ToArray())
                .ToArray();

            var anodes = new HashSet<Coordinate>();

            void CalculateAntinodes(Coordinate position, Coordinate direction)
            {
                do
                {
                    position += direction;
                    anodes.Add(position);
                }
                while (grid.IsInGrid(width, height, position) && moreAntinodes);
            }

            foreach (var frequencies in frequenciesGroups)
            {
                var coordinatePairs = GetCoordinatePairs(frequencies).ToArray();
                
                foreach (var (a, b) in coordinatePairs)
                {
                    var topNode = a.Y <= b.Y ? a : b;
                    var bottomNode = topNode == a ? b : a;
                    
                    var xDistance = topNode.X - bottomNode.X;
                    var yDistance = bottomNode.Y - topNode.Y;

                    CalculateAntinodes(topNode, new Coordinate(xDistance, -yDistance));
                    CalculateAntinodes(bottomNode, new Coordinate(-xDistance, yDistance));
                }

                if (moreAntinodes)
                {
                    foreach (var c in frequencies)
                    {
                        anodes.Add(c);
                    }
                }
            }

            return anodes.Count(c => grid.IsInGrid(width, height, c));
        }

        private static IEnumerable<(Coordinate A, Coordinate B)> GetCoordinatePairs(Coordinate[] coords)
        {
            for (var i = 0; i < coords.Length; i++)
            {
                for (var j = i + 1; j < coords.Length; j++)
                {
                    yield return (coords[i], coords[j]);
                }
            }
        }
    }
}
