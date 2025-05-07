using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoTracker
{
    public class BingoBoard
    {
        private int?[,] grid = new int?[3, 9];
        private bool[,] marks = new bool[3, 9];
        public string ID { get; }

        public int CompletedRows
        {
            get
            {
                return Enumerable.Range(0, 3)
                                 .Count(r => IsRowCompleted(r));
            }
            set
            {

            }
        }


        public bool FullCard
        {
            get => CompletedRows == 3;
            set
            {

            }
        }

        public BingoBoard(string id)
        {
            ID = id;
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            int seed = ID.GetHashCode();
            var rnd = new Random(seed);


            int[] colCount = Enumerable.Repeat(1, 9).ToArray();
            int toDistribute = 15 - 9;
            while (toDistribute > 0)
            {
                int c = rnd.Next(9);
                if (colCount[c] < 3)
                {
                    colCount[c]++;
                    toDistribute--;
                }
            }


            int[][] rowSlots = AssignRows(colCount, rnd);


            for (int c = 0; c < 9; c++)
            {
                int start = (c == 0 ? 1 : c * 10);
                int end = (c == 0 ? 9 : c * 10 + 9);

                var numbers = Enumerable.Range(start, end - start + 1)
                                        .OrderBy(_ => rnd.Next())
                                        .Take(colCount[c])
                                        .OrderBy(x => x)
                                        .ToArray();

                for (int k = 0; k < colCount[c]; k++)
                    grid[rowSlots[c][k], c] = numbers[k];
            }
        }

        private int[][] AssignRows(int[] colCount, Random rnd)
        {
            var result = new int[9][];
            int[] rowCounts = { 5, 5, 5 };

            bool Recurse(int col)
            {
                if (col == 9)
                    return rowCounts.All(rc => rc == 0);

                var combos = GetCombinations(new[] { 0, 1, 2 }, colCount[col])
                             .OrderBy(_ => rnd.Next());

                foreach (var combo in combos)
                {
                    if (combo.All(r => rowCounts[r] > 0))
                    {
                        combo.ForEach(r => rowCounts[r]--);
                        result[col] = combo.ToArray();

                        if (Recurse(col + 1))
                            return true;

                        combo.ForEach(r => rowCounts[r]++);
                    }
                }
                return false;
            }

            if (!Recurse(0))
                throw new InvalidOperationException("Kunne ikke fordele rækker korrekt.");

            return result;
        }

        private List<List<int>> GetCombinations(int[] arr, int k)
        {
            var res = new List<List<int>>();
            void Combine(int start, List<int> curr)
            {
                if (curr.Count == k)
                {
                    res.Add(new List<int>(curr));
                    return;
                }
                for (int i = start; i < arr.Length; i++)
                {
                    curr.Add(arr[i]);
                    Combine(i + 1, curr);
                    curr.RemoveAt(curr.Count - 1);
                }
            }
            Combine(0, new List<int>());
            return res;
        }

        public void MarkNumber(int number)
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 9; c++)
                    if (grid[r, c] == number)
                        marks[r, c] = true;
        }

        public bool IsRowCompleted(int r)
            => Enumerable.Range(0, 9)
                         .Where(c => grid[r, c].HasValue)
                         .All(c => marks[r, c]);

        public int? GetNumber(int r, int c) => grid[r, c];
        public bool IsMarked(int r, int c) => marks[r, c];
    }
}
