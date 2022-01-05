using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        const int BOARD_SIZE = 5;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var draft = input[0].Split(',').Select(int.Parse).ToList();
            var boards = new List<Board>();
            for (int i = 2; i < input.Length; i += BOARD_SIZE + 1)
            {
                var numbers = new int[BOARD_SIZE][];
                for (int j = 0; j < BOARD_SIZE; j++)
                    numbers[j] = input[i + j].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                boards.Add(new Board(numbers));
            }

            var firstWinningBoardScore = -1;
            var lastWinningBoardScore = -1;
            for (int i = 0; i < draft.Count; i++)
            {
                var winningBoards = new List<Board>();
                foreach (var board in boards)
                {
                    if (board.HasWon(draft.GetRange(0, i + 1).ToArray(), out List<int> unmarked))
                    {
                        winningBoards.Add(board);
                        var score = unmarked.Sum() * draft[i];

                        if (firstWinningBoardScore == -1)
                            firstWinningBoardScore = score;
                        lastWinningBoardScore = score;
                    }
                }

                boards = boards.Where(b => !winningBoards.Contains(b)).ToList();
            }
            Console.WriteLine($"First board score: {firstWinningBoardScore}");
            Console.WriteLine($"Last board score: {lastWinningBoardScore}");
            Console.ReadLine();
        }

        class Board
        {
            private int[,] numbers;

            public Board(int[][] numbers)
            {
                this.numbers = new int[numbers.Length, numbers.Length];
                for (int y = 0; y < numbers.Length; y++)
                    for (int x = 0; x < numbers.Length; x++)
                        this.numbers[x, y] = numbers[y][x];
            }

            public bool HasWon(int[] draft, out List<int> unmarked)
            {
                unmarked = new List<int>();
                bool hasWon = false;
                for(int i = 0; i < numbers.GetLength(0); i++)
                {
                    unmarked.AddRange(GetRow(i).Where(n => !draft.Contains(n)));
                    var markedInRow = GetRow(i).Count(n => draft.Contains(n));
                    var markedInColumn = GetColumn(i).Count(n => draft.Contains(n));

                    if (markedInRow == numbers.GetLength(0) || markedInColumn == numbers.GetLength(0))
                        hasWon = true;
                }
                return hasWon;
            }

            int[] GetRow(int index) => Enumerable.Range(0, numbers.GetLength(0)).Select(y => numbers[index, y]).ToArray();
            int[] GetColumn(int index) => Enumerable.Range(0, numbers.GetLength(0)).Select(x => numbers[x, index]).ToArray();
        }
    }
}
