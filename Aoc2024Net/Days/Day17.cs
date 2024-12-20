using System.Text.RegularExpressions;

namespace Aoc2024Net.Days
{
    internal sealed class Day17 : Day
    {
        public override object? SolvePart1()
        {
            var (a, b, c, program) = GetInputData();
            var output = GetProgramOutput(a, b, c, program);
            return string.Join(",", output);
        }

        public override object? SolvePart2() =>
            null;

        private (long A, long B, long C, int[] Program) GetInputData()
        {
            var lines = InputData
                .GetInputLines()
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();

            long GetNumber(string line) =>
                long.Parse(Regex.Match(line, @"\d+").Value);

            var a = GetNumber(lines[0]);
            var b = GetNumber(lines[1]);
            var c = GetNumber(lines[2]);

            var program = Regex
                .Match(lines[3], @"Program: (.+)")
                .Groups[1]
                .Value
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            return (a, b, c, program);
        }

        private ICollection<long> GetProgramOutput(long a, long b, long c, int[] program)
        {
            long GetLiteralOperandValue(long operand) =>
                operand;

            long GetComboOperandValue(long operand) => operand switch
            {
                >= 0 and <= 3 => GetLiteralOperandValue(operand),
                4 => a,
                5 => b,
                6 => c,
            };

            var output = new List<long>();

            for (var i = 0; i < program.Length - 1; i += 2)
            {
                var opcode = program[i];
                var operand = program[i + 1];

                switch (opcode)
                {
                    case 0:
                        a /= 1 << (int)GetComboOperandValue(operand);
                        break;
                    case 1:
                        b ^= GetLiteralOperandValue(operand);
                        break;
                    case 2:
                        b = GetComboOperandValue(operand) % 8;
                        break;
                    case 3:
                        if (a == 0)
                            continue;

                        i = (int)GetLiteralOperandValue(operand) - 2;
                        break;
                    case 4:
                        b ^= c;
                        break;
                    case 5:
                        output.Add(GetComboOperandValue(operand) % 8);
                        break;
                    case 6:
                        b = a / (1 << (int)GetComboOperandValue(operand));
                        break;
                    case 7:
                        c = a / (1 << (int)GetComboOperandValue(operand));
                        break;
                }
            }

            return output;
        }
    }
}
