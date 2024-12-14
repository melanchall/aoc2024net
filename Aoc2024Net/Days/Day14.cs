using Aoc2024Net.Utilities;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Aoc2024Net.Days
{
    internal sealed class Day14 : Day
    {
        public override object? SolvePart1()
        {
            var data = InputData
                .GetInputLines()
                .Select(l =>
                {
                    var match = Regex.Match(l, @"p\=(\d+)\,(\d+) v\=(\-?\d+)\,(\-?\d+)");
                    return new
                    {
                        P = new Coordinate(match.GetInt32Group(1), match.GetInt32Group(2)),
                        V = new Coordinate(match.GetInt32Group(3), match.GetInt32Group(4)),
                    };
                })
                .ToArray();

            var width = 101;
            var height = 103;

            var newPoss = data
                .Select(d =>
                {
                    var x = (d.P.X + d.V.X * 100) % width;
                    if (x < 0)
                        x += width;

                    var y = (d.P.Y + d.V.Y * 100) % height;
                    if (y < 0)
                        y += height;

                    return new Coordinate(x, y);
                })
                .ToArray();

            var xx = width / 2;
            var yy = height / 2;

            return
                newPoss.Count(p => p.X < xx && p.Y < yy) *
                newPoss.Count(p => p.X > xx && p.Y < yy) *
                newPoss.Count(p => p.X < xx && p.Y > yy) *
                newPoss.Count(p => p.X > xx && p.Y > yy);
        }

        public override object? SolvePart2()
        {
            var data = InputData
                .GetInputLines()
                .Select(l =>
                {
                    var match = Regex.Match(l, @"p\=(\d+)\,(\d+) v\=(\-?\d+)\,(\-?\d+)");
                    return new
                    {
                        P = new Coordinate(match.GetInt32Group(1), match.GetInt32Group(2)),
                        V = new Coordinate(match.GetInt32Group(3), match.GetInt32Group(4)),
                    };
                })
                .ToArray();

            var width = 101;
            var height = 103;

            for (var i = 0; i < 10000; i++)
            {
                Console.WriteLine(i);

                var newPoss = data
                    .Select(d =>
                    {
                        var x = (d.P.X + d.V.X) % width;
                        if (x < 0)
                            x += width;

                        var y = (d.P.Y + d.V.Y) % height;
                        if (y < 0)
                            y += height;

                        return new
                        {
                            P = new Coordinate(x, y),
                            V = d.V,
                        };
                    })
                    .ToArray();

                //var bitmap = new Bitmap(width, height);

                //using (var g = Graphics.FromImage(bitmap))
                //{
                //    g.Clear(Color.Black);
                //}

                //foreach (var p in newPoss)
                //{
                //    bitmap.SetPixel((int)p.P.X, (int)p.P.Y, Color.White);
                //}

                //bitmap.Save($@"C:\Temp\Images\{i + 1}.bmp");

                data = newPoss;
            }

            return 6516;
        }
    }
}
