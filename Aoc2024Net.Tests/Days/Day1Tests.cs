using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"3   4
                    4   3
                    2   5
                    1   3
                    3   9
                    3   3", 11)]
    [DayDataPart1(1189304)]
    [DayDataPart2(@"3   4
                    4   3
                    2   5
                    1   3
                    3   9
                    3   3", 31)]
    [DayDataPart2(24349736)]
    [TestFixture]
    public sealed class Day1Tests : DayTests<Day1Tests>
    {
    }
}
