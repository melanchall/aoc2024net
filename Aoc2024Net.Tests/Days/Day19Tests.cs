using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"r, wr, b, g, bwu, rb, gb, br

                    brwrr
                    bggr
                    gbbr
                    rrbgbr
                    ubwu
                    bwurrg
                    brgr
                    bbrgwb", 6)]
    [DayDataPart1(304)]
    [DayDataPart2(@"r, wr, b, g, bwu, rb, gb, br

                    brwrr
                    bggr
                    gbbr
                    rrbgbr
                    ubwu
                    bwurrg
                    brgr
                    bbrgwb", 16)]
    [DayDataPart2(705756472327497)]
    [TestFixture]
    public sealed class Day19Tests : DayTests<Day19Tests>
    {
    }
}
