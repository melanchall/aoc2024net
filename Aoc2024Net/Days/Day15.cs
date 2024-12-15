using Aoc2024Net.Utilities;
using System.Text.RegularExpressions;

namespace Aoc2024Net.Days
{
    internal sealed class Day15 : Day
    {
        private sealed class Object
        {
            public Object(long x, long y, int width = 1)
            {
                X = x;
                Y = y;
                Width = width;
            }

            public long X { get; set; }

            public long Y { get; set; }

            public int Width { get; set; }

            public override int GetHashCode() =>
                (int)(X * 100000 + Y);

            public override bool Equals(object? obj) =>
                obj is Object point &&
                point.X == X &&
                point.Y == Y;
        }

        private static readonly Dictionary<char, Coordinate> Moves = new ()
        {
            ['<'] = new (-1, 0),
            ['>'] = new (1, 0),
            ['^'] = new (0, -1),
            ['v'] = new (0, 1),
        };

        public override object? SolvePart1() =>
            Solve(false);

        public override object? SolvePart2() =>
            Solve(true);

        private long Solve(bool useWideMap)
        {
            var linesGroups = InputData.GetInputLinesGroups();
            
            var (robot, boxes, walls) = GetData(linesGroups[0], useWideMap);
            var moves = string.Join(string.Empty, linesGroups[1]);

            foreach (var move in moves)
            {
                var direction = Moves[move];

                var objectsToMove = GetObjectsToMove(direction, robot, boxes, walls);
                if (!objectsToMove.Any())
                    continue;

                var lastObj = objectsToMove.Last();
                var objAfterLast = new Object(
                    direction.X == -1 ? lastObj.X - 1 : (direction.X == 1 ? lastObj.X + lastObj.Width : lastObj.X),
                    lastObj.Y + direction.Y);
                if (walls.Contains(objAfterLast))
                    continue;

                foreach (var obj in objectsToMove)
                {
                    obj.X += direction.X;
                    obj.Y += direction.Y;
                }
            }

            return boxes.Sum(b => b.Y * 100 + b.X);
        }

        private static IEnumerable<Object> GetObjectsToMove(
            Coordinate direction,
            Object robot,
            ICollection<Object> boxes,
            ICollection<Object> walls)
        {
            var objectsToMove = new HashSet<Object>();
            var queue = new Queue<Object>();
            queue.Enqueue(robot);

            while (queue.Any())
            {
                var obj = queue.Dequeue();
                objectsToMove.Add(obj);

                var objectsToEnqueue = Enumerable.Empty<Object>();

                if (direction.Y == 0)
                {
                    var nextX = obj.X + direction.X;
                    var box = boxes.FirstOrDefault(b => !objectsToMove.Contains(b) && b.Y == obj.Y && ((nextX >= b.X && nextX < b.X + b.Width) || (direction.X == 1 && nextX + obj.Width - 1 >= b.X && nextX + obj.Width - 1 < b.X + b.Width)));
                    objectsToEnqueue = [box];

                    if (walls.Any(w => w.X == nextX && w.Y == obj.Y + direction.Y))
                        return Enumerable.Empty<Object>();
                }
                else
                {
                    var nextY = obj.Y + direction.Y;
                    objectsToEnqueue = boxes
                        .Where(b => !objectsToMove.Contains(b) && b.Y == nextY && ((obj.X >= b.X && obj.X < b.X + b.Width) || (obj.X + obj.Width - 1 >= b.X && obj.X + obj.Width - 1 < b.X + b.Width)))
                        .ToArray();

                    if (walls.Any(w => w.Y == nextY && w.X >= obj.X && w.X < obj.X + obj.Width))
                        return Enumerable.Empty<Object>();
                }

                foreach (var nextObj in objectsToEnqueue.Where(obj => obj != null))
                {
                    queue.Enqueue(nextObj);
                }
            }

            return objectsToMove;
        }

        private static (Object Robot, ICollection<Object> Boxes, ICollection<Object> Walls) GetData(string[] gridLines, bool useWideMap)
        {
            if (useWideMap)
                gridLines = gridLines
                    .Select(l => Regex.Replace(l, @"[#@O\.]", m => m.Value switch
                    {
                        "#" => "##",
                        "@" => "@.",
                        "O" => "[]",
                        _ => ".."
                    }))
                    .ToArray();

            var width = gridLines[0].Length;
            var height = gridLines.Length;

            var grid = new char[width, height];
            
            var robot = default(Object);
            var walls = new List<Object>();
            var boxes = new List<Object>();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var sym = gridLines[y][x];
                    var p = new Object(x, y);

                    if (sym == '@')
                        robot = p;
                    else if (sym == '#')
                        walls.Add(p);
                    else if (sym == 'O')
                        boxes.Add(p);
                    else if (sym == '[')
                        boxes.Add(new Object(x, y, 2));
                }
            }

            return (robot, boxes, walls);
        }
    }
}
