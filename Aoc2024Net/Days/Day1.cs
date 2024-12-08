namespace Aoc2024Net.Days
{
    internal sealed class Day1 : Day
    {
        public override object? SolvePart1()
        {
            var (firstList, secondList) = GetLists();
            return firstList
                .OrderBy(x => x)
                .Zip(secondList.OrderBy(y => y), (x, y) => Math.Abs(x - y))
                .Sum();
        }

        public override object? SolvePart2()
        {
            var (firstList, secondList) = GetLists();
            return firstList.Select(x => x * secondList.Count(y => y == x)).Sum();
        }

        private (int[] FirstList, int[] SecondList) GetLists()
        {
            var pairs = InputData
                .GetInputLines()
                .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Select(p => (X: int.Parse(p[0]), Y: int.Parse(p[1])))
                .ToArray();

            var first = pairs.Select(p => p.X).ToArray();
            var second = pairs.Select(p => p.Y).ToArray();

            return (first, second);
        }
    }
}
