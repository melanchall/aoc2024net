using Aoc2024Net.Utilities;
using System.Text.RegularExpressions;

namespace Aoc2024Net.Days
{
    internal sealed class Day13 : Day
    {
        private record Rule(
            Coordinate ButtonAShift,
            Coordinate ButtonBShift,
            Coordinate PrizeLocation);

        public override object? SolvePart1() =>
            Solve(false);

        public override object? SolvePart2() =>
            Solve(true);

        private long? Solve(bool useBigLocations)
        {
            var rules = InputData
                .GetInputLinesGroups()
                .Select(g =>
                {
                    var buttonAMatch = Regex.Match(g[0], @"Button A\: X\+(\d+), Y\+(\d+)");
                    var buttonBMatch = Regex.Match(g[1], @"Button B\: X\+(\d+), Y\+(\d+)");
                    var prizeMatch = Regex.Match(g[2], @"Prize\: X\=(\d+), Y\=(\d+)");

                    return new Rule(
                        new Coordinate(
                            buttonAMatch.GetInt32Group(1),
                            buttonAMatch.GetInt32Group(2)),
                        new Coordinate(
                            buttonBMatch.GetInt32Group(1),
                            buttonBMatch.GetInt32Group(2)),
                        new Coordinate(
                            (useBigLocations ? 10000000000000 : 0) + prizeMatch.GetInt32Group(1),
                            (useBigLocations ? 10000000000000 : 0) + prizeMatch.GetInt32Group(2)));
                })
                .ToArray();

            return rules
                .Select(CalculateMinWinCost)
                .Where(cost => cost != null)
                .Sum();
        }

        private static long? CalculateMinWinCost(Rule rule)
        {
            // We have the equations system:
            //
            // aX* x +bX * y = lX
            // aY* x +bY * y = lY
            // 
            // x - A-button clicks count
            // y - B-button clicks count
            //
            // Let's solve it by summin both equations
            // to eliminate x first and found y

            var xyLcm = LeastCommonMultiple(rule.ButtonAShift.X, rule.ButtonAShift.Y);
            var xFactor = xyLcm / rule.ButtonAShift.X;
            var yFactor = xyLcm / rule.ButtonAShift.Y;

            var bX = rule.ButtonBShift.X * xFactor;
            var bY = -rule.ButtonBShift.Y * yFactor;
            
            var lX = rule.PrizeLocation.X * xFactor;
            var lY = -rule.PrizeLocation.Y * yFactor;

            var y = (lX + lY) / (bX + bY);
            var x = (rule.PrizeLocation.X - (rule.ButtonBShift.X * y)) / rule.ButtonAShift.X;

            if (rule.ButtonAShift.X * x + rule.ButtonBShift.X * y != rule.PrizeLocation.X ||
                rule.ButtonAShift.Y * x + rule.ButtonBShift.Y * y != rule.PrizeLocation.Y)
                return null;

            return x * 3 + y;
        }

        // https://stackoverflow.com/a/13569863/2975589
        public static long LeastCommonMultiple(long a, long b)
        {
            long num1, num2;

            if (a > b)
            {
                num1 = a;
                num2 = b;
            }
            else
            {
                num1 = b;
                num2 = a;
            }

            for (var i = 1; i < num2; i++)
            {
                var mult = num1 * i;
                if (mult % num2 == 0)
                {
                    return mult;
                }
            }

            return num1 * num2;
        }
    }
}
