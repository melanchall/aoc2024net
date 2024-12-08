namespace Aoc2024Net.Days
{
    internal sealed class Day5 : Day
    {
        public override object? SolvePart1()
        {
            var (rules, updates) = GetRulesAndUpdates();
            return updates.Where(u => IsCorrect(u, rules)).Sum(u => u[u.Length / 2]);
        }

        public override object? SolvePart2()
        {
            var (rules, updates) = GetRulesAndUpdates();

            var incorrectUpdates = updates.Where(u => !IsCorrect(u, rules)).ToArray();
            var result = 0;

            foreach (var update in incorrectUpdates)
            {
                while (!IsCorrect(update, rules))
                {
                    for (var i = 0; i < update.Length; i++)
                    {
                        var j = Enumerable
                            .Range(0, i)
                            .FirstOrDefault(k => rules.Contains($"{update[i]}|{update[k]}"), -1);

                        if (j >= 0)
                            (update[j], update[i]) = (update[i], update[j]);
                    }
                }

                result += update[update.Length / 2];
            }

            return result;
        }

        private (string[] Rules, int[][] Updates) GetRulesAndUpdates()
        {
            var groups = InputData.GetInputLinesGroups();

            var rules = groups[0];
            var updates = groups[1].Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();

            return (rules, updates);
        }

        private static bool IsCorrect(int[] update, string[] rules) => Enumerable
            .Range(0, update.Length)
            .All(i => !update[..i].Any(n => rules.Contains($"{update[i]}|{n}")));
    }
}
