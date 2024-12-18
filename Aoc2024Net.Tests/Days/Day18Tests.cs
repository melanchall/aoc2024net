using Aoc2024Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2024Net.Tests.Days
{
    [DayDataPart1(@"5,4
                    4,2
                    4,5
                    3,0
                    2,1
                    6,3
                    2,4
                    1,5
                    0,6
                    3,3
                    2,6
                    5,1
                    1,2
                    5,5
                    2,5
                    6,5
                    1,4
                    0,4
                    6,4
                    1,1
                    6,1
                    1,0
                    0,5
                    1,6
                    2,0", 22, Parameters = [7, 12])]
    [DayDataPart1(null, 370, Parameters = [71, 1024])]
    [DayDataPart2(@"5,4
                    4,2
                    4,5
                    3,0
                    2,1
                    6,3
                    2,4
                    1,5
                    0,6
                    3,3
                    2,6
                    5,1
                    1,2
                    5,5
                    2,5
                    6,5
                    1,4
                    0,4
                    6,4
                    1,1
                    6,1
                    1,0
                    0,5
                    1,6
                    2,0", "6,1", Parameters = [7, 12])]
    [DayDataPart2(null, "65,6", Parameters = [71, 1024])]
    [TestFixture]
    public sealed class Day18Tests : DayTests<Day18Tests>
    {
    }
}
