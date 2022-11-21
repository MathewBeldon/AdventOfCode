string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayEight\input.txt").ToArray();

int partOneResult = PartOne(arr);
int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(string[] arr)
{
    int total = 0;
    int[] countable = new int[] {2, 3, 4, 7};
    foreach(string part in arr)
    {
        var result = ParseLine(part);
        total += result.input.Count(x => countable.Contains(x.Length));
    }
    return total;
}

int PartTwo(string[] arr)
{
    int total = 0;
    foreach (string part in arr)
    {
        string together = string.Empty;

        var result = ParseLine(part);
        foreach (string number in result.input)
        {
            together += result.table.Where(x => x.Value == number)?.First().Key.ToString() ?? string.Empty;
        }
        total += int.Parse(together);
    }
    return total;
}

(Dictionary<int, string> table, string[] input) ParseLine(string line)
{
    string[] splitline = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
    List<string> signals = splitline[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new string(Sort(x.ToArray()))).ToList();
    string[] input = splitline[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new string(Sort(x.ToArray()))).ToArray();

    Dictionary<int, string> map = new Dictionary<int, string>();
    for (int i = 0; i < signals.Count; i++)
    {
        char[] sigArr = signals[i].ToArray();
        Array.Sort(sigArr);
        string sig = new string(sigArr);

        switch (signals[i].Length)
        {
            case 2:
                map.Add(1, sig);
                break;
            case 3:
                map.Add(7, sig);
                break;
            case 4: 
                map.Add(4, sig);
                break;
            case 7:
                map.Add(8, sig);
                break;
        }
    }

    foreach (string sig in signals.Where(x => x.Length == 5))
    {
        char[] sigArr = sig.ToArray();

        if (AinB(sigArr, map[4].ToArray()) == 3 &&
            AinB(sigArr, map[1].ToArray()) == 1)
        {
            map.Add(5, sig);
        } 
        else if (AinB(sigArr, map[4].ToArray()) == 2 &&
                 AinB(sigArr, map[1].ToArray()) == 1)
        {
            map.Add(2, sig);
        }
        else if (AinB(sigArr, map[4].ToArray()) == 3 &&
                 AinB(sigArr, map[1].ToArray()) == 2) 
        { 
            map.Add(3, sig);
        }
    }

    foreach (string sig in signals.Where(x => x.Length == 6))
    {
        char[] sigArr = sig.ToArray();

        if (AinB(sigArr, map[5].ToArray()) == 5 &&
            AinB(sigArr, map[4].ToArray()) == 3)
        {
            map.Add(6, sig);
        }
        else if (AinB(sigArr, map[5].ToArray()) == 5 &&
                 AinB(sigArr, map[4].ToArray()) == 4)
        {
            map.Add(9, sig);
        }
        else if (AinB(sigArr, map[5].ToArray()) == 4 &&
                 AinB(sigArr, map[4].ToArray()) == 3)
        {
            map.Add(0, sig);
        }
    }

    return (map, input);
}

int AinB(char[] A, char[] B)
{
    int counter = 0;
    foreach(char a in A)
    {
        foreach (char b in B) 
        {
            if (a == b)
            {
                counter++;
            }
        }
    }
    return counter;
}

char[] Sort(char[] chars)
{
    Array.Sort(chars);
    return chars;
}