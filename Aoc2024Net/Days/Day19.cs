namespace Aoc2024Net.Days
{
    internal sealed class Day19 : Day
    {
        public override object? SolvePart1() =>
            GetArrangementCounts().Count;

        public override object? SolvePart2() =>
            GetArrangementCounts().Sum(c => c.Value);

        private Dictionary<string, long> GetArrangementCounts()
        {
            var linesGroups = InputData.GetInputLinesGroups();

            var towels = linesGroups[0][0].Split(',', StringSplitOptions.TrimEntries).ToArray();
            var designs = linesGroups[1];

            var counts = new Dictionary<string, long>();

            foreach (var design in designs)
            {
                GetArrangementCounts(design, towels, counts);
            }

            return counts
                .Where(c => designs.Contains(c.Key) && c.Value > 0)
                .ToDictionary(
                    c => c.Key,
                    c => c.Value);
        }

        private static long GetArrangementCounts(
            string design,
            string[] towels,
            Dictionary<string, long> counts)
        {
            if (counts.TryGetValue(design, out var c))
                return c;

            var result = towels.LongCount(t => t == design);

            foreach (var t in towels)
            {
                if (!design.StartsWith(t))
                    continue;

                result += GetArrangementCounts(design[t.Length..], towels, counts);
            }

            return counts[design] = result;
        }
    }
}
