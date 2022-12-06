using Shared;

var arr = Helper.ParseRangeToStringArray(5, 0, 8)
    .Select(x => x.Replace("    ", " "))
    .Select(x => x.Replace("] ", string.Empty))
    .Select(x => x.Replace("[", string.Empty))
    .Select(x => x.Replace("]", string.Empty))
    .Select(x => x.TrimEnd())
    .Reverse();

var stack = new Dictionary<int, Stack<char>>();

foreach (var item in arr)
{
    if (string.IsNullOrWhiteSpace(item)) {
        break;
    };

    int counter = 0;
    foreach(var character in item)
    {
        if (!stack.ContainsKey(counter))
        {
            stack.Add(counter, new Stack<char>());
        }

        if (character != ' ')
        {
            stack[counter].Push(character);
        }
        counter++;
    }
}

var moves = Helper.ParseRangeToStringArray(5, 10, 999)
    .Select(x => x.Replace("move ", string.Empty))
    .Select(x => x.Replace(" from ", "-"))
    .Select(x => x.Replace(" to ", "-"))
    .Select(x => x.Split('-')
    .Select(int.Parse)
    .ToArray());

var stack2 = stack.ToDictionary(x => x.Key, x => new Stack<char>(x.Value.Reverse()));
foreach (var move in moves)
{
    var tmp = new List<char>();
    for (int i = 0; i < move[0]; i++)
    {
        stack[move[2] - 1].Push(stack[move[1] - 1].Pop());
        tmp.Add(stack2[move[1] - 1].Pop());
    }
    tmp.Reverse();
    foreach (var crate in tmp)
    {
        stack2[move[2] - 1].Push(crate);
    }
}

var total = string.Empty;
var total2 = string.Empty;

foreach (var item in stack)
{
    total += item.Value.Pop();
}

foreach (var item in stack2)
{
    total2 += item.Value.Pop();
}
Console.WriteLine($"Part One: {total}");
Console.WriteLine($"Part Two: {total2}");

Console.ReadLine();