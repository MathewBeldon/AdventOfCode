List<int> input = File.ReadAllText(@"C:\src\AOC\AdventOfCodeDaySix\input.txt").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

int partOneResult = PartOne(new List<int>(input), 80);
long partTwoResult = PartTwo(new List<int>(input), 256);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(List<int> input, int maxDays)
{
    return CalculateToDay(input, 0);

    int CalculateToDay(List<int> input, int day)
    {   
        int total = input.Count;

        for (int i = 0; i < total; i++)
        {
            if (input[i] == 0)
            {
                input[i] = 6;
                input.Add(8);
            }
            else
            {
                input[i]--;
            }
        }

        if (++day < maxDays)
        {
            CalculateToDay(input, day);
        }

        return input.Count;
    }
}

long PartTwo(List<int> input, int maxDay)
{
    long[] counter = new long[9];

    foreach (int i in input)
    {
        counter[i]++;
    }

    return CalculateToDay(counter, 0);

    long CalculateToDay(long[] input, int day)
    {
        long tmp = input[0];

        for (int i = 1; i < input.Length; i++)
        {
            input[i - 1] = input[i];
        }
        
        input[6] += tmp;
        input[8] = tmp;

        if (++day < maxDay)
        {
            CalculateToDay(input, day);
        }
        return input.Sum();
    }
}
