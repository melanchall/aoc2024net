using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"..X...
                    .SAMX.
                    .A..A.
                    XMAS.S
                    .X....", 4)]
    [DayDataPart1(@"MMMSXXMASM
                    MSAMXMSMSA
                    AMXSXMAAMM
                    MSAMASMSMX
                    XMASAMXAMM
                    XXAMMXXAMA
                    SMSMSASXSS
                    SAXAMASAAA
                    MAMMMXMMMM
                    MXMXAXMASX", 18)]
    [DayDataPart1(2401)]
    [DayDataPart2(@".M.S......
                    ..A..MSMS.
                    .M.S.MAA..
                    ..A.ASMSM.
                    .M.S.M....
                    ..........
                    S.S.S.S.S.
                    .A.A.A.A..
                    M.M.M.M.M.
                    ..........", 9)]
    [DayDataPart2(1822)]
    [TestFixture]
    public sealed class Day4Tests : DayTests<Day4Tests>
    {
    }
}
