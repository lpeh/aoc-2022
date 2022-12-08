namespace AdventOfCode.Day2;

static class Day2
{
    public static int Run(string inputPath)
    {
        
        Console.WriteLine(string.Join(", ", trees));
        var visibleTreeCount = width + width + height + height - 4;
        for (int j = 0; j < height; j++) {
            for (int i = 0; i < width; i++) {
                Console.Write(trees[j * height + i]);
            }
            Console.WriteLine();
        }
        Console.WriteLine($"There are {visibleTreeCount} trees visible from outside the grid");
        return 0;
    }

    IEnumerable<int[]> GetRows(List<int> grid)
    {

    }    

    class Grid 
    {
        public Grid(string path)
        {
            var trees = new List<int>();
            int columns = 0;
            int rows = 0;
            foreach (var line in File.ReadAllLines(path))
            {
                if (columns == 0) {
                    columns = line.Length;
                } else if (columns != line.Length) {
                    throw new InvalidDataException($"Line {line} expected length {columns}, got {line.Length}.");
                }
                foreach (var c in line) {
                    trees.Add((int)(c - '0'));
                }
                rows++;
            }
            Trees = trees.ToArray();
            Columns = columns;
            Rows = rows;
        }

        public int[] Trees { get; }
        public int Columns { get; }
        public int Rows { get; }
        
        public IEnumerable<int> GetRow(int row)
        {
            for (int i = 0; i < Columns; i++) {
                yield return GetHeight(i, row);
            }
        }     


        public IEnumerable<int> GetColumn(int column)
        {
            for (int j = 0; j < Rows; j++) {
                yield return GetHeight(column, j);
            }   
        }

        public int GetHeight(int i, int j) => Trees[j * Columns + i];
    }
}