using Shared;

var arr = Helper.ParseToStringArray(4);
var sections = arr.Select(x => x.Split(',')).ToList();
var total = 0;
var total2 = 0;

foreach (var section in sections)
{
    var firstSection = section[0].Split('-').Select(int.Parse).ToArray();
    var secondSection = section[1].Split('-').Select(int.Parse).ToArray();

    if (firstSection[0] <= secondSection[0] && firstSection[1] >= secondSection[1] ||
        secondSection[0] <= firstSection[0] && secondSection[1] >= firstSection[1])
    {
        total++;
    }

    if (firstSection[1] >= secondSection[0] && firstSection[0] <= secondSection[1])
    {
        total2++;
    }
}

Console.WriteLine($"Part One: {total}");
Console.WriteLine($"Part Two: {total2}");

Console.ReadLine();