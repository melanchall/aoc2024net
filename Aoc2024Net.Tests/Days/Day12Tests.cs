using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"AAAA
                    BBCD
                    BBCC
                    EEEC", 140)]
    [DayDataPart1(@"OOOOO
                    OXOXO
                    OOOOO
                    OXOXO
                    OOOOO", 772)]
    [DayDataPart1(@"RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE", 1930)]
    [DayDataPart1(1304764)]
    [DayDataPart2(@"AAAA
                    BBCD
                    BBCC
                    EEEC", 80)]
    [DayDataPart2(@"OOOOO
                    OXOXO
                    OOOOO
                    OXOXO
                    OOOOO", 436)]
    [DayDataPart2(@"EEEEE
                    EXXXX
                    EEEEE
                    EXXXX
                    EEEEE", 236)]
    [DayDataPart2(@"AAAAAA
                    AAABBA
                    AAABBA
                    ABBAAA
                    ABBAAA
                    AAAAAA", 368)]
    [DayDataPart2(@"RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE", 1206)]
    [DayDataPart2(811148)]
    [TestFixture]
    public sealed class Day12Tests : DayTests<Day12Tests>
    {
    }
}
