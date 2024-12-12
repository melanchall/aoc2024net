using Aoc2024Net.Utilities;

namespace Aoc2024Net.Days
{
    internal sealed class Day12 : Day
    {
        private record Edge(Coordinate From, Coordinate To);

        private record Region(Coordinate[] Coordinates, Edge[] Edges);

        private static readonly Coordinate[] Moves =
        [
            new Coordinate(1, 0),
            new Coordinate(-1, 0),
            new Coordinate(0, 1),
            new Coordinate(0, -1),
        ];

        public override object? SolvePart1() =>
            GetRegions().Sum(r => r.Coordinates.Length * r.Edges.Length);

        public override object? SolvePart2()
        {
            var regions = GetRegions();
            var result = 0;

            foreach (var region in regions)
            {
                var sortedEdges = region
                    .Edges
                    .OrderBy(e => e.From.Y)
                    .ThenBy(e => e.From.X)
                    .ToList();

                var sidesCount = 0;

                while (sortedEdges.Count > 0)
                {
                    var edge = sortedEdges.First();

                    while (edge != null)
                    {
                        sortedEdges.Remove(edge);

                        var nextEdge = sortedEdges.FirstOrDefault(e =>
                            e.From == edge.To &&
                            (edge.From.X == edge.To.X ? e.From.X == e.To.X : e.From.Y == e.To.Y) &&
                            region.Edges.Count(ee => ee != edge && (e.From == ee.To || e.From == ee.From)) < 3);

                        edge = nextEdge;
                    }

                    sidesCount++;
                }

                result += region.Coordinates.Length * sidesCount;
            }

            return result;
        }

        private ICollection<Region> GetRegions() =>
            GetRegions(GetRegionsCoordinates());

        private ICollection<Coordinate[]> GetRegionsCoordinates()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();

            var visited = new HashSet<Coordinate>();
            var regionsCoordinates = new List<Coordinate[]>();

            foreach (var c in grid.GetAllCoordinates())
            {
                if (visited.Contains(c))
                    continue;

                var plantType = grid.At(c);
                var regionCoordinates = new List<Coordinate>();

                var queue = new Queue<Coordinate>();
                queue.Enqueue(c);

                while (queue.Any())
                {
                    var currentCoordinate = queue.Dequeue();
                    if (!visited.Add(currentCoordinate))
                        continue;

                    regionCoordinates.Add(currentCoordinate);

                    foreach (var move in Moves)
                    {
                        var nextCoordinate = currentCoordinate + move;
                        if (grid.IsInGrid(width, height, nextCoordinate) && grid.At(nextCoordinate) == plantType)
                            queue.Enqueue(nextCoordinate);
                    }
                }

                regionsCoordinates.Add(regionCoordinates.ToArray());
            }

            return regionsCoordinates;
        }

        private static ICollection<Region> GetRegions(ICollection<Coordinate[]> regionsCoordinates)
        {
            var regions = new List<Region>();

            foreach (var regionCoordinates in regionsCoordinates)
            {
                var plotsEdges = new List<Edge>();

                foreach (var c in regionCoordinates)
                {
                    plotsEdges.AddRange(
                    [
                        new Edge(new Coordinate(c.X, c.Y), new Coordinate(c.X, c.Y + 1)),
                        new Edge(new Coordinate(c.X + 1, c.Y), new Coordinate(c.X + 1, c.Y + 1)),
                        new Edge(new Coordinate(c.X, c.Y), new Coordinate(c.X + 1, c.Y)),
                        new Edge(new Coordinate(c.X, c.Y + 1), new Coordinate(c.X + 1, c.Y + 1))
                    ]);
                }

                var borderEdges = plotsEdges
                    .GroupBy(e => e)
                    .Where(g => g.Count() == 1)
                    .Select(g => g.First())
                    .ToArray();

                regions.Add(new Region(regionCoordinates, borderEdges));
            }

            return regions;
        }
    }
}
