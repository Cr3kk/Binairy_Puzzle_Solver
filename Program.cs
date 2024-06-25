using System;

namespace BinaryPuzzleSolver
{
    class Program
    {
        public static void Main()
        {
            int[,] grid = new int[6, 6] 
            {
                { 0, 0, 1, 1, 0, 1},
                { 1, 0, 1,-1,-1, 0},
                { 0, 1, 0,-1,-1, 1},
                { 1, 0, 1, 0, 0, 1},
                { 1, 1, 0,-1,-1, 0},
                { 0, 1, 0, 1, 1, 0}
            };

            Console.WriteLine("Initial grid:");
            PrintGrid(grid);

            int iterations = 0;
            int[,] newGrid = new int[grid.GetLength(0), grid.GetLength(1)];

            do 
            {
                newGrid = (int[,])grid.Clone();
                iterations++;
                grid = Solve.solveForDoublesRows(grid);
                grid = Solve.solveForDoublesCols(grid);
                grid = Solve.solveForGapsRows(grid);
                grid = Solve.solveForGapsCols(grid);
                grid = Solve.solveForRowsMinOne(grid);
                grid = Solve.solveForColsMinOne(grid);
                grid = Solve.ensureUniqueRows(grid);
                grid = Solve.ensureUniqueCols(grid);
            } while (!GridsAreEqual(newGrid, grid));

            Console.WriteLine($"Solved grid in {iterations} iterations:");
            PrintGrid(grid);
        }

        static void PrintGrid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == -1)
                        Console.Write(". ");
                    else
                        Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static bool GridsAreEqual(int[,] grid1, int[,] grid2)
        {
            for (int i = 0; i < grid1.GetLength(0); i++)
            {
                for (int j = 0; j < grid1.GetLength(1); j++)
                {
                    if (grid1[i, j] != grid2[i, j])
                        return false;
                }
            }
            return true;
        }
    }

    class Solve
    {
        public static int[,] solveForDoublesRows(int[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1) - 2; col++)
                {
                    if (grid[row, col] == grid[row, col + 1] && grid[row, col] != -1)
                    {
                        if (grid[row, col + 2] == -1)
                        {
                            grid[row, col + 2] = 1 - grid[row, col];
                        }
                        if (col > 0 && grid[row, col - 1] == -1)
                        {
                            grid[row, col - 1] = 1 - grid[row, col];
                        }
                    }
                }
            }
            return grid;
        }

        public static int[,] solveForDoublesCols(int[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                for (int row = 0; row < grid.GetLength(0) - 2; row++)
                {
                    if (grid[row, col] == grid[row + 1, col] && grid[row, col] != -1)
                    {
                        if (grid[row + 2, col] == -1)
                        {
                            grid[row + 2, col] = 1 - grid[row, col];
                        }
                        if (row > 0 && grid[row - 1, col] == -1)
                        {
                            grid[row - 1, col] = 1 - grid[row, col];
                        }
                    }
                }
            }
            return grid;
        }

        public static int[,] solveForGapsRows(int[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1) - 2; col++)
                {
                    if (grid[row, col] == grid[row, col + 2] && grid[row, col] != -1)
                    {
                        if (grid[row, col + 1] == -1)
                        {
                            grid[row, col + 1] = 1 - grid[row, col];
                        }
                    }
                }
            }
            return grid;
        }

        public static int[,] solveForGapsCols(int[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                for (int row = 0; row < grid.GetLength(0) - 2; row++)
                {
                    if (grid[row, col] == grid[row + 2, col] && grid[row, col] != -1)
                    {
                        if (grid[row + 1, col] == -1)
                        {
                            grid[row + 1, col] = 1 - grid[row, col];
                        }
                    }
                }
            }
            return grid;
        }

        public static int[,] solveForRowsMinOne(int[,] grid)
        {
            int halfLength = grid.GetLength(1) / 2;

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                int zerosFound = 0;
                int onesFound = 0;

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 0) { zerosFound++; }
                    else if (grid[row, col] == 1) { onesFound++; }
                }

                if (zerosFound == halfLength)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (grid[row, col] == -1)
                        {
                            grid[row, col] = 1;
                        }
                    }
                }
                else if (onesFound == halfLength)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (grid[row, col] == -1)
                        {
                            grid[row, col] = 0;
                        }
                    }
                }
            }

            return grid;
        }

        public static int[,] solveForColsMinOne(int[,] grid)
        {
            int halfLength = grid.GetLength(0) / 2;

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int zerosFound = 0;
                int onesFound = 0;

                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    if (grid[row, col] == 0) { zerosFound++; }
                    else if (grid[row, col] == 1) { onesFound++; }
                }

                if (zerosFound == halfLength)
                {
                    for (int row = 0; row < grid.GetLength(0); row++)
                    {
                        if (grid[row, col] == -1)
                        {
                            grid[row, col] = 1;
                        }
                    }
                }
                else if (onesFound == halfLength)
                {
                    for (int row = 0; row < grid.GetLength(0); row++)
                    {
                        if (grid[row, col] == -1)
                        {
                            grid[row, col] = 0;
                        }
                    }
                }
            }

            return grid;
        }

        public static int[,] ensureUniqueRows(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = i + 1; j < grid.GetLength(0); j++)
                {
                    if (RowsAreEqual(grid, i, j))
                    {
                        for (int col = 0; col < grid.GetLength(1); col++)
                        {
                            if (grid[i, col] == -1 || grid[j, col] == -1)
                            {
                                grid[j, col] = 1 - grid[i, col];
                            }
                        }
                    }
                }
            }
            return grid;
        }

        public static int[,] ensureUniqueCols(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = i + 1; j < grid.GetLength(1); j++)
                {
                    if (ColsAreEqual(grid, i, j))
                    {
                        for (int row = 0; row < grid.GetLength(0); row++)
                        {
                            if (grid[row, i] == -1 || grid[row, j] == -1)
                            {
                                grid[row, j] = 1 - grid[row, i];
                            }
                        }
                    }
                }
            }
            return grid;
        }

        static bool RowsAreEqual(int[,] grid, int row1, int row2)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row1, col] != grid[row2, col] && grid[row1, col] != -1 && grid[row2, col] != -1)
                {
                    return false;
                }
            }
            return true;
        }

        static bool ColsAreEqual(int[,] grid, int col1, int col2)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (grid[row, col1] != grid[row, col2] && grid[row, col1] != -1 && grid[row, col2] != -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
