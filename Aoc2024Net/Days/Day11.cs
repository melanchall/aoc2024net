namespace Aoc2024Net.Days
{
    internal sealed class Day11 : Day
    {
        public override object? SolvePart1() =>
            Solve(25);

        public override object? SolvePart2() =>
            Solve(75);

        private long Solve(int blinkingCount)
        {
            var numbers = InputData
                .GetInputText()
                .Split(' ')
                .Select(long.Parse)
                .ToArray();

            var stonesCountsCache = new Dictionary<(long, int), long>();
            return numbers.Sum(n => CountStones(n, blinkingCount, stonesCountsCache));
        }

        private static long CountStones(long number, int blinkingCount, Dictionary<(long, int), long> stonesCountsCache)
        {
            if (blinkingCount == 0)
                return 1;

            if (stonesCountsCache.TryGetValue((number, blinkingCount), out var r))
                return r;

            var result = 0L;
            if (number == 0)
                result = CountStones(1, blinkingCount - 1, stonesCountsCache);
            else
            {
                var s = number.ToString();
                if (s.Length % 2 == 0)
                    result =
                        CountStones(long.Parse(s[..(s.Length / 2)]), blinkingCount - 1, stonesCountsCache) +
                        CountStones(long.Parse(s[(s.Length / 2)..]), blinkingCount - 1, stonesCountsCache);
                else
                    result = CountStones(number * 2024, blinkingCount - 1, stonesCountsCache);
            }

            return stonesCountsCache[(number, blinkingCount)] = result;
        }
    }
}
