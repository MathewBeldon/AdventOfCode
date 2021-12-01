int[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayOne\input.txt").Select(x => int.Parse(x)).ToArray();

int partOneResult = PartOne(arr);
int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult} increases");
Console.WriteLine($"Part Two: {partTwoResult} increases");

int PartOne(int[] arr)
{
    int increase = 0;

    for (int i = 0; i < arr.Length - 1; i++)
    {
        if (arr[i] < arr[i + 1])
        {
            increase++;
        }
    }
    return increase;
}

int PartTwo(int[] arr)
{
    const int SLIDING_WINDOW_LENGTH = 3;
    int increase = 0;

    int firstSum = arr[0..SLIDING_WINDOW_LENGTH].Sum();
    int secondSum = firstSum;

    for (int i = 1; i <= arr.Length - SLIDING_WINDOW_LENGTH; i++)
    {
        firstSum = secondSum;
        secondSum = arr[i..(i + SLIDING_WINDOW_LENGTH)].Sum();
        if (firstSum < secondSum)
        {
            increase++;
        }
    }
    return increase;
}
