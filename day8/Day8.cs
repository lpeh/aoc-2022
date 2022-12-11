namespace AdventOfCode.Day8;

static class Day8
{
    public static int Run(string inputPath)
    {
        var grid = Grid.FromFile(inputPath);
        var visibleTreeCount = 0;
        var visibleTrees = new bool[grid.Trees.Length];
        foreach (var (i, j) in grid.GetVisibleTrees())
        {
            visibleTrees[j * grid.Columns + i] = true;
            visibleTreeCount++;
        }
        {
            int n = 0;
            for (var j = 0; j < grid.Rows; j++)
            {
                for (var i = 0; i < grid.Columns; i++)
                {
                    if (visibleTrees[n])
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write('-');
                    }
                    n++;
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine();
        {
            int n = 0;
            for (var j = 0; j < grid.Rows; j++)
            {
                for (var i = 0; i < grid.Columns; i++)
                {
                    Console.ForegroundColor = visibleTrees[n] ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write($"{grid.Trees[n]}");
                    n++;
                }
                Console.WriteLine();
            }
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"There are {visibleTreeCount} trees visible from outside the grid");
        return 0;
    }

    class Grid
    {
        public Grid()
        {
            Trees = Array.Empty<int>();
        }

        public static Grid FromFile(string path)
        {
            var trees = new List<int>();
            int cols = 0, rows = 0;
            foreach (var line in File.ReadAllLines(path))
            {
                if (cols == 0)
                {
                    cols = line.Length;
                }
                else if (cols != line.Length)
                {
                    throw new InvalidDataException($"Line {line} expected length {cols}, got {line.Length}.");
                }
                foreach (var height in line)
                {
                    if (height >= '0' && height <= '9') {
                        trees.Add((int)(height - '0'));
                    } else {
                        throw new InvalidDataException("Not a number.");
                    }
                }
                rows++;
            }
            return new Grid
            {
                Trees = trees.ToArray(),
                Columns = cols,
                Rows = rows
            };
        }

        public int[] Trees { get; init; }

        public int Columns { get; init; }

        public int Rows { get; init; }

        public IEnumerable<(int i, int j)> GetVisibleTrees()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (IsEdge(i, j) ||
                        IsVisibleHorizontally(i, j) ||
                        IsVisibleVertically(i, j))
                    {
                        yield return (i, j);
                    }
                }
            }
        }

        bool IsEdge(int i, int j)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (i == 0 || i == Columns - 1)
            {
                return true;
            }
            if (j == 0 || j == Rows - 1)
            {
                return true;
            }
            return false;
        }

        bool IsEmpty => Trees.Length == 0;

        bool IsVisibleHorizontally(int i, int j)
        {
            var trees = GetRow(j);
            return IsVisibleFromStart(trees, i) || IsVisibleFromEnd(trees, i);
        }

        bool IsVisibleVertically(int i, int j)
        {
            var trees = GetColumn(i);
            return IsVisibleFromStart(trees, j) || IsVisibleFromEnd(trees, j);
        }

        bool IsVisibleFromStart(int[] trees, int n)
        {
            if (IsEdge(trees, n))
            {
                return true;
            }
            var height = trees[n];
            do
            {
                if (trees[n - 1] >= height)
                {
                    return false;
                }
                n--;
            } while (n > 0);
            return true;
        }

        bool IsVisibleFromEnd(int[] trees, int n)
        {
            if (IsEdge(trees, n))
            {
                return true;
            }
            var height = trees[n];
            do
            {
                if (trees[n + 1] >= height)
                {
                    return false;
                }
                n++;
            } while (n < trees.Length - 1);
            return true;
        }

        bool IsEdge(int[] trees, int n)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (n == 0 || n == trees.Length - 1)
            {
                return true;
            }
            return false;
        }

        public int[] GetRow(int j)
        {
            var row = new int[Columns];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = GetTreeHeight(i, j);
            }
            return row;
        }

        public int[] GetColumn(int i)
        {
            var column = new int[Rows];
            for (int j = 0; j < column.Length; j++)
            {
                column[j] = GetTreeHeight(i, j);
            }
            return column;
        }

        public int GetTreeHeight(int i, int j) => Trees[j * Columns + i];
    }
}