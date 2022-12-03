using Shared;

var arr = Helper<char>.SplitParseToArray(2, ' ');

var dict = new Dictionary<char, int>()
{
    {'A', 1},
    {'B', 2},
    {'C', 3},
    {'X', 1},
    {'Y', 2},
    {'Z', 3},
};

var winning = new Dictionary<int, int>()
{
    {1, 3},
    {2, 1},
    {3, 2}
};

var losing = new Dictionary<int, int>()
{
    {3, 1},
    {1, 2},
    {2, 3}
};

int total = 0;
foreach (var game in arr)
{
    if (dict[game[0]] == winning[dict[game[1]]])
    {
        total += dict[game[1]] + 6;
        continue;
    }
    if (dict[game[0]] == dict[game[1]])
    {
        total += dict[game[1]] + 3;
        continue;
    }
    total += dict[game[1]];
}

int total2 = 0;
foreach (var game in arr)
{
    if (game[1] == 'X')
    {
        total2 += winning[dict[game[0]]];
        continue;
    }
    if (game[1] == 'Z')
    {
        total2 += losing[dict[game[0]]] + 6;
        continue;
    }
    total2 += dict[game[0]] + 3;
}

Console.WriteLine("Part One: " + total);
Console.WriteLine("Part Two: " + total2);

Console.ReadLine();
