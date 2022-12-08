using Shared;

var arr = Helper.ParseToStringArray(8)
    .Select(x => x.ToCharArray()
        .Select(x => int.Parse(x.ToString())).ToArray())
    .ToArray();

var trees = new Dictionary<(int, int), int[]>();
var visibleTrees = new HashSet<(int, int)>();

for (int i = 1; i < arr.Length - 1; i++)
{
    for (int j = 1; j < arr[i].Length - 1; j++)
    {
        if (!trees.ContainsKey((i, j))) trees.Add((i, j), new int[4]);

        // go up
        for (int up = i - 1; up >= 0; up--)
        {
            trees[(i, j)][0]++;
            if (arr[up][j] >= arr[i][j]) break;
            if (up == 0) visibleTrees.Add((i, j));
        }

        // go left
        for (int left = j - 1; left >= 0; left--)
        {
            trees[(i, j)][1]++;
            if (arr[i][left] >= arr[i][j]) break;
            if (left == 0) visibleTrees.Add((i, j));
        }

        // go down
        for (int down = i + 1; down <= arr.Length - 1; down++)
        {
            trees[(i, j)][2]++;
            if (arr[down][j] >= arr[i][j]) break;
            if (down == arr.Length - 1) visibleTrees.Add((i, j));
        }

        // go right
        for (int right = j + 1; right <= arr[i].Length - 1; right++)
        {
            trees[(i, j)][3]++;
            if (arr[i][right] >= arr[i][j]) break;
            if (right == arr[i].Length - 1) visibleTrees.Add((i, j));
        }
    }
}

var total = visibleTrees.Count + (arr.Length * 2) + (arr[0].Length * 2) - 4;
var total2 = trees.Select(x => x.Value[0] * x.Value[1] * x.Value[2] * x.Value[3]).Max();

Console.WriteLine($"Part One: {total}");
Console.WriteLine($"Part One: {total2}");

Console.ReadLine();