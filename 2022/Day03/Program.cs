using Shared;

var arr = Helper.ParseToStringArray(3);

var group = new List<List<int>>();
var total = 0;
var total2 = 0;

foreach (var item in arr)
{
    var itemList = new List<int>();
    foreach (var letter in item)
    {
        if (char.IsUpper(letter))
        {
            itemList.Add(letter - 38);
            continue;
        }
        itemList.Add(letter - 96);
    }

    var firstHalf = itemList.Skip(itemList.Count / 2);
    var secondHalf = itemList.Take(itemList.Count / 2);
    total += firstHalf.Intersect(secondHalf).Single();
    
    group.Add(itemList);
    if (group.Count > 2)
    {
        total2 += group[0].Intersect(group[1]).Intersect(group[2]).Single();
        group.Clear();
    }
}

Console.WriteLine($"Part One: {total}");
Console.WriteLine($"Part Two: {total2}");

Console.ReadLine();