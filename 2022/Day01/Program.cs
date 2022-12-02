using Shared;

var arr = Helper.ParseToStringArray(1);

List<int> calories = new List<int>();

int total = 0;
foreach (var elf in arr)
{
    if (!string.IsNullOrWhiteSpace(elf))
    {
        total += int.Parse(elf);
        continue;
    }

    calories.Add(total);
    total = 0;
}

calories.Sort();
calories.Reverse();

Console.WriteLine("Part One: " + calories[0]);
Console.WriteLine("Part Two: " + calories.GetRange(0, 3).Sum());

Console.ReadLine();