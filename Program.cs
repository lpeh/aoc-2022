using AdventOfCode.Day1;
using AdventOfCode.Day2;

try {
    if (args.Length == 0) {
        Console.WriteLine("aoc <day> <input path>");
        return -1; 
    }
    var inputPath = args[1];
    if (!File.Exists(inputPath)) {
        throw new FileNotFoundException("Input file not found.", inputPath);
    }
    if (!uint.TryParse(args[0], out var day) || !(day >= 1 || day <= 25)) {
        throw new ArgumentException($"Invalid day {args[1]}");
    }

    switch (day) {
        case 1: return Day1.Run(inputPath);
        case 2: return Day2.Run(inputPath);
        default: throw new NotSupportedException();
    }
} catch (Exception ex) {
    Console.WriteLine(ex);
    return -1;
}