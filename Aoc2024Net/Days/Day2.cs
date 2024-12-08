namespace Aoc2024Net.Days
{
    internal sealed class Day2 : Day
    {
        public override object? SolvePart1()
        {
            var reports = GetReports();
            return reports.Count(IsSafe);
        }

        public override object? SolvePart2()
        {
            var reports = GetReports();
            return reports.Count(r =>
                IsSafe(r) ||
                Enumerable.Range(0, r.Length).Any(i => IsSafe([.. r[..i], .. r[(i + 1)..]])));
        }

        private int[][] GetReports() => InputData
            .GetInputLines()
            .Select(l => l.Split(' ').Select(int.Parse).ToArray())
            .ToArray();

        private bool IsSafe(int[] report)
        {
            var diffs = report[..^1].Zip(report[1..], (x, y) => x - y).ToArray();
            return diffs.All(d =>
                Math.Abs(d) >= 1 &&
                Math.Abs(d) <= 3 &&
                Math.Sign(d) == Math.Sign(diffs[0]));
        }
    }
}
