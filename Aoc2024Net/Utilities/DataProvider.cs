﻿namespace Aoc2024Net.Utilities
{
    internal static class DataProvider
    {
        public static IEnumerable<(int I, int J)> GetIndicesPairs(int minIndex, int maxIndex)
        {
            for (var i = minIndex; i <= maxIndex - 1; i++)
            {
                for (var j = i + 1; j <= maxIndex; j++)
                {
                    yield return (i, j);
                }
            }
        }

        public static IEnumerable<(int I, int J)> GetFullIndicesPairs(int minIndex, int maxIndex)
        {
            for (var i = minIndex; i <= maxIndex; i++)
            {
                for (var j = minIndex; j <= maxIndex; j++)
                {
                    yield return (i, j);
                }
            }
        }

        public static IEnumerable<Coordinate> GetGridCoordinates(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    yield return new(x, y);
                }
            }
        }
    }
}
