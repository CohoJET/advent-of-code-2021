using System;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();

            Console.ReadLine();
        }

        static void PartOne()
        {
            var course = File.ReadAllLines("input.txt");

            var horizontal = 0;
            var depth = 0;

            foreach(var line in course)
            {
                var command = line.Split(' ');
                var argument = int.Parse(command[1]);

                switch(command[0])
                {
                    case "forward":
                        horizontal += argument;
                        break;
                    case "down":
                        depth += argument;
                        break;
                    case "up":
                        depth -= argument;
                        break;
                }
            }

            Console.WriteLine($"Part One result: {horizontal * depth} increases");
        }

        static void PartTwo()
        {
            var course = File.ReadAllLines("input.txt");

            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var line in course)
            {
                var command = line.Split(' ');
                var argument = int.Parse(command[1]);

                switch (command[0])
                {
                    case "forward":
                        horizontal += argument;
                        depth += aim * argument;
                        break;
                    case "down":
                        aim += argument;
                        break;
                    case "up":
                        aim -= argument;
                        break;
                }
            }

            Console.WriteLine($"Part Two result: {horizontal * depth} increases");
        }
    }
}
