int[] arr = File.ReadAllText(@"C:\src\AOC\AdventOfCodeDaySeven\input.txt").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

Array.Sort(arr);
var median = arr[arr.Length / 2];
var mean = arr.Sum() / arr.Length;

int partOneResult = PartOne(arr, median);
int partTwoResult = PartTwo(arr, mean);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(int[] arr, int median)
{
    int total = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        total += Math.Abs(arr[i] - median);
    }
    return total;
}

int PartTwo(int[] arr, int mean)
{
    int total = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        int range = Math.Abs(arr[i] - mean);
        for (int j = 1; j <= range; j++)
        {
            total += j;
        }
    }
    return total;
}