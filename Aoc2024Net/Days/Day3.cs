using System.Text.RegularExpressions;

namespace Aoc2024Net.Days
{
    internal sealed class Day3 : Day
    {
        public override object? SolvePart1() => Solve(false);

        public override object? SolvePart2() => Solve(true);

        private long Solve(bool useSwitchInstructions)
        {
            const string disableInstruction = "don't()";
            const string enableInstruction = "do()";

            var input = InputData.GetInputText();
            var matches = Regex.Matches(
                input,
                @$"mul\((\d{{1,3}}),(\d{{1,3}})\){(useSwitchInstructions ? @$"|{Regex.Escape(disableInstruction)}|{Regex.Escape(enableInstruction)}" : string.Empty)}");

            var enabled = true;
            var result = 0;

            foreach (Match m in matches)
            {
                if (m.Value == disableInstruction)
                    enabled = false;
                else if (m.Value == enableInstruction)
                    enabled = true;
                else if (enabled)
                    result += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
            }

            return result;
        }
    }
}
