using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
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
            var report = File.ReadAllLines("input.txt");

            int[] counter = new int[report[0].Length];
            foreach (var line in report)
                for (int i = 0; i < line.Length; i++)
                    counter[i] += line[i].Equals('1') ? 1 : -1;

            var gamma = string.Empty;
            var epsilon = string.Empty;
            foreach (var digit in counter)
            {
                var isOne = digit > 0;
                gamma += isOne ? "1" : "0";
                epsilon += isOne ? "0" : "1";
            }

            var result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
            Console.WriteLine($"Part One result: {result}");
        }

        static void PartTwo()
        {
            var report = File.ReadAllLines("input.txt");

            var o2list = new List<string>(report);
            var co2list = new List<string>(report);

            var o2criteria = 'X';
            var co2criteria = 'X';

            var o2rating = string.Empty;
            var co2rating = string.Empty;

            for (int i = 0; i < report[0].Length + 1; i++)
            {
                var o2counter = 0;
                var co2counter = 0;

                foreach (var line in report)
                {
                    var delta = 0;
                    if (i < report[0].Length)
                        delta = line[i].Equals('1') ? 1 : -1;

                    if (o2list.Contains(line) && (i == 0 || line[i - 1].Equals(o2criteria)))
                        o2counter += delta;
                    else o2list.Remove(line);

                    if (co2list.Contains(line) && (i == 0 || line[i - 1].Equals(co2criteria)))
                        co2counter += delta;
                    else co2list.Remove(line);
                }

                o2criteria = o2counter >= 0 ? '1' : '0';
                co2criteria = co2counter >= 0 ? '0' : '1';

                if (o2list.Count == 1)
                    o2rating = o2list[0];
                if (co2list.Count == 1)
                    co2rating = co2list[0];
            }

            var result = Convert.ToInt32(o2rating, 2) * Convert.ToInt32(co2rating, 2); ;
            Console.WriteLine($"Part Two result: {result}");
        }
    }
}
