using Shared;

var arr = Helper.ParseToStringArray(10);

int cycle = 0;
int eax = 1;
int[] stops = new int[6] { 20, 60, 100, 140, 180, 220 };
int total = 0;
int row = 0;
const int SCREEN_WIDTH = 40;

foreach (var item in arr)
{
    
    if (item.StartsWith("addx"))
    {
        for (int i = 0; i < 2; i++)
        {
            CheckCycle();
        }
        eax += int.Parse(item.Replace("addx ", string.Empty));
        continue;
    }
    CheckCycle();
}

Console.WriteLine("\nPart Two ^");
Console.WriteLine($"Part One: {total}");
Console.ReadLine();

void CheckCycle()
{
    if (cycle >= SCREEN_WIDTH * row)
    {
        Console.WriteLine();
        row++;
    }
    cycle++;
    if (Enumerable.Range(eax, 3).Contains(cycle - ((row - 1) * SCREEN_WIDTH)))
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(".");
    }
    if (stops.Contains(cycle))
    {
        total += cycle * eax;
    }
}