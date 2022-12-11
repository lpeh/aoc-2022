using AdventOfCode.Day1;
using AdventOfCode.Day8;

try {
    if (args.Length <= 1) {
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
        case 8: return Day8.Run(inputPath);
        default: throw new NotSupportedException();
    }
} catch (Exception ex) {
    Console.WriteLine(ex);
    return -1;
}