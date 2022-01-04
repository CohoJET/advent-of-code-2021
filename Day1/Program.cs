using System;
using System.IO;

namespace Day1
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
            var report = Array.ConvertAll(File.ReadAllLines("input.txt"), s => int.Parse(s));

            var result = 0;
            for (int i = 1; i < report.Length; i++)
                result += report[i] > report[i - 1] ? 1 : 0;

            Console.WriteLine($"Part One result: {result} increases");
        }

        static void PartTwo()
        {
            var report = Array.ConvertAll(File.ReadAllLines("input.txt"), s => int.Parse(s));

            var result = 0;
            for (int i = 0; i < report.Length - 3; i++)
            {
                var window1 = report[i] + report[i + 1] + report[i + 2];
                var window2 = report[i + 1] + report[i + 2] + report[i + 3];
                result += window2 > window1 ? 1 : 0;
            }

            Console.WriteLine($"Part Two result: {result} increases");
        }
    }
}
