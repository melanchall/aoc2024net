namespace Aoc2024Net.Days
{
    internal sealed class Day7 : Day
    {
        private enum Operator
        {
            Add,
            Multiply,
            Concatenate,
        }

        private static readonly Operator[] SimpleOperatorsSet = [Operator.Add, Operator.Multiply];
        private static readonly Operator[] ExtendedOperatorsSet = [Operator.Add, Operator.Multiply, Operator.Concatenate];

        public override object? SolvePart1() =>
            Solve(false);

        public override object? SolvePart2() =>
            Solve(true);

        private long Solve(bool useExtendedOperatorsSet)
        {
            var equations = InputData
                .GetInputLines()
                .Select(l => l.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Select(p => (long.Parse(p[0]), p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray()))
                .ToArray();

            var result = 0L;
            var cache = new Operator[equations.Max(e => e.Item2.Length)][][];

            foreach (var (testValue, numbers) in equations)
            {
                var operatorsSets = GetOperatorsSets(numbers.Length - 1, cache, useExtendedOperatorsSet).ToArray();

                foreach (var set in operatorsSets)
                {
                    var value = new[] { Operator.Add }
                        .Concat(set)
                        .Zip(numbers, (o, n) => (o, n))
                        .Aggregate(0L, (res, x) => x.o == Operator.Add ? res + x.n : (x.o == Operator.Concatenate ? res * (long)Math.Pow(10, x.n.ToString().Length) + x.n : res * x.n));

                    if (value == testValue)
                    {
                        result += testValue;
                        break;
                    }
                }
            }

            return result;
        }

        private static Operator[][] GetOperatorsSets(int places, Operator[][][] cache, bool useExtendedOperatorsSet)
        {
            var operatorsSet = useExtendedOperatorsSet
                ? ExtendedOperatorsSet
                : SimpleOperatorsSet;

            if (places == 1)
                return operatorsSet.Select(s => new[] { s }).ToArray();

            if (cache[places] != null)
                return cache[places];

            return cache[places] = operatorsSet
                .SelectMany(op => GetOperatorsSets(places - 1, cache, useExtendedOperatorsSet)
                    .Select(s => new[] { op }
                    .Concat(s)
                    .ToArray()))
                .ToArray();
        }
    }
}
