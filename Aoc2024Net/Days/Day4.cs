namespace Aoc2024Net.Days
{
    internal sealed class Day4 : Day
    {
        public override object? SolvePart1()
        {
            const int margin = 3;

            var (grid, width, height) = InputData.GetInputCharGrid(margin, ' ');
            var result = 0;

            static int IsXmas(char x, char m, char a, char s) =>
                Convert.ToInt32($"{x}{m}{a}{s}" == "XMAS");

            for (var x = margin; x < width + margin; x++)
            {
                for (var y = margin; y < height + margin; y++)
                {
                    result +=
                        IsXmas(grid[x, y], grid[x + 1, y], grid[x + 2, y], grid[x + 3, y]) +
                        IsXmas(grid[x, y], grid[x - 1, y], grid[x - 2, y], grid[x - 3, y]) +
                        IsXmas(grid[x, y], grid[x, y + 1], grid[x, y + 2], grid[x, y + 3]) +
                        IsXmas(grid[x, y], grid[x, y - 1], grid[x, y - 2], grid[x, y - 3]) +
                        IsXmas(grid[x, y], grid[x + 1, y + 1], grid[x + 2, y + 2], grid[x + 3, y + 3]) +
                        IsXmas(grid[x, y], grid[x + 1, y - 1], grid[x + 2, y - 2], grid[x + 3, y - 3]) +
                        IsXmas(grid[x, y], grid[x - 1, y - 1], grid[x - 2, y - 2], grid[x - 3, y - 3]) +
                        IsXmas(grid[x, y], grid[x - 1, y + 1], grid[x - 2, y + 2], grid[x - 3, y + 3]);
                }
            }

            return result;
        }

        public override object? SolvePart2()
        {
            const int margin = 2;

            var (grid, width, height) = InputData.GetInputCharGrid(margin, ' ');
            var result = 0;

            static int IsXmas(char leftTop, char rightTop, char middle, char leftBottom, char rightBottom) =>
                Convert.ToInt32(
                    ($"{leftTop}{middle}{rightBottom}" == "MAS" || $"{leftTop}{middle}{rightBottom}" == "SAM") &&
                    ($"{rightTop}{middle}{leftBottom}" == "MAS" || $"{rightTop}{middle}{leftBottom}" == "SAM"));

            for (var x = margin; x < width + margin; x++)
            {
                for (var y = margin; y < height + margin; y++)
                {
                    result += IsXmas(grid[x, y], grid[x + 2, y], grid[x + 1, y + 1], grid[x, y + 2], grid[x + 2, y + 2]);
                }
            }

            return result;
        }
    }
}
