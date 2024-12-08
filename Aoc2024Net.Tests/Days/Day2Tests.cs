using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"7 6 4 2 1
                    1 2 7 8 9
                    9 7 6 2 1
                    1 3 2 4 5
                    8 6 4 4 1
                    1 3 6 7 9", 2)]
    [DayDataPart1(220)]
    [DayDataPart2(@"7 6 4 2 1
                    1 2 7 8 9
                    9 7 6 2 1
                    1 3 2 4 5
                    8 6 4 4 1
                    1 3 6 7 9", 4)]
    [DayDataPart2(296)]
    [TestFixture]
    public sealed class Day2Tests : DayTests<Day2Tests>
    {
    }
}
