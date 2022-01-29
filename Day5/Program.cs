using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(l => l.Split(" -> ")).ToList();
            var lines = new List<Line>();
            for(int i = 0; i < input.Count; i ++)
            {
                var p0 = input[i][0].Split(",");
                var p1 = input[i][1].Split(",");
                lines.Add(new Line(new Point(int.Parse(p0[0]), int.Parse(p0[1])), new Point(int.Parse(p1[0]), int.Parse(p1[1]))));
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var map = new Dictionary<Point, int>();
            foreach (var line in lines)
            {
                if (!line.IsStraight())
                    continue;

                foreach (var point in line.GetPoints())
                {
                    if (!map.TryAdd(point, 1))
                        map[point] += 1;
                }
            }

            var straightLinesIntersections = 0;
            foreach (var pair in map)
                if (pair.Value >= 2)
                    straightLinesIntersections++;

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds + " ms");
            
            Console.WriteLine($"Total intersections: {straightLinesIntersections}");
            Console.ReadLine();
        }

        struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Point Interpolate(Point point, double ratio) => new Point((int)((point.x - x) * ratio) + x, (int)((point.y - y) * ratio) + y);
            public int Distance(Point point) => Math.Abs(point.x - x) + Math.Abs(point.y - y);

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                return x == ((Point)obj).x && y == ((Point)obj).y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(x.GetHashCode(), y.GetHashCode());
            }
        }
        class Line
        {
            public Point p0, p1;
            public List<Point> Points;

            public Line(Point p0, Point p1)
            {
                this.p0 = p0;
                this.p1 = p1;
                //Points = GetPoints();
            }

            public bool IsStraight() => p0.x == p1.x || p0.y == p1.y;
            public List<Point> GetPoints()
            {
                var points = new List<Point>() { p0, p1 };
                var distance = p0.Distance(p1);
                var step = (1d / distance);
                for (int i = 1; i < distance; i++)
                    points.Add(p0.Interpolate(p1, step * i));
                //Console.WriteLine($"{p0.x},{p0.y}:{points[points.Count - 1].x},{points[points.Count - 1].y}:{p1.x},{p1.y}");
                return points;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                return p0.Equals(((Line)obj).p0) && p1.Equals(((Line)obj).p1);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(p0.GetHashCode(), p1.GetHashCode());
            }
        }
    }
}
