namespace AdventOfCode.Day1;

static class Day1
{
    public static int Run(string inputPath)
    {
        int currentElf = 1;
        int currentSum = 0;
        int elfCarryingMostCalories = 0;
        int mostCalories = -1;
        using var file = File.OpenText(inputPath);
        do {
            var line = file.ReadLine();
            if (line is null) {
                break;
            }
            if (line.Length == 0) {
                currentSum = 0;
                currentElf++;
                continue;
            }
            currentSum += int.Parse(line);
            if (currentSum > mostCalories) {
                elfCarryingMostCalories = currentElf;
                mostCalories = currentSum;
            }
        } while(true);
        Console.WriteLine($"The Elf #{elfCarryingMostCalories} carries total {mostCalories} calories.");
        return 0;
    }
}