string[] lines = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayTen\input.txt").ToArray();

char[] open = new char[] { '(', '[', '{', '<' };
char[] close = new char[] { ')', ']', '}', '>' };


var result = ParseInput(lines);

int partOneResult = PartOne(result.InvalidChars);
long partTwoResult = PartTwo(result.FinishedLines);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(List<char> invalidChars)
{
    int[] points = new int[] { 3, 57, 1197, 25137 };
    int total = 0;
    foreach(char c in invalidChars)
    {
        total += points[Array.IndexOf(close, c)];
    }
    return 0;
}

long PartTwo(List<string> lines)
{    
    List<long> totals = new List<long>();    
    foreach(var line in lines)
    {
        int[] points = new int[] { 1, 2, 3, 4 };
        long total = 0;
        foreach(char c in line)
        {
            total *= 5;
            total += points[Array.IndexOf(close, c)];
        }
        totals.Add(total);
    }
    totals.Sort();
    return totals[totals.Count / 2];
}

(List<char> InvalidChars, List<string> FinishedLines) ParseInput(string[] input)
{
    var invalidChars = new List<char>();
    var finishedLines = new List<string>();
    
    for (int line = 0; line < lines.Length; line++)
    {
        bool skip = false;
        Stack<char> stack = new Stack<char>();
        for (int c = 0; c < lines[line].Length; c++)
        {
            if (open.Contains(lines[line][c]))
            {
                stack.Push(lines[line][c]);
            }
            else
            {
                if (lines[line][c] == close[Array.IndexOf(open, stack.Peek())])
                {
                    stack.Pop();
                }
                else
                {
                    invalidChars.Add(lines[line][c]);
                    skip = true;
                    break;
                }
            }
        }
        if (!skip)
        {
            int stackCount = stack.Count;
            string remainingChars = string.Empty;
            for (int i = 0; i < stackCount; i++)
            {
                remainingChars += close[Array.IndexOf(open, stack.Pop())];
            }
            finishedLines.Add(remainingChars);
        }
    }
    return (invalidChars, finishedLines);
}

