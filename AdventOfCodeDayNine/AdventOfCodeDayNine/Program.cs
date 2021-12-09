string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayNine\input.txt").ToArray();

int[][] map = new int[arr.Length][];
for (int i = 0; i < arr.Length; i++)
{
    map[i] = arr[i].Select(x => int.Parse(x.ToString())).ToArray();
}

int partOneResult = PartOne(map);
int partTwoResult = PartTwo(map);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(int[][] map)
{
    List<int> lowPoint = new List<int>();

    int total = 0;
    for(int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            List<int> adjacent = new List<int>();
            if (x > 0)
            {
                adjacent.Add(map[y][x - 1]);
            }
            if (x + 1 < map[y].Length)
            {
                adjacent.Add(map[y][x + 1]);
            }
            if (y > 0)
            {
                adjacent.Add(map[y - 1][x]);
            }
            if (y + 1 < map.Length)
            {
                adjacent.Add(map[y + 1][x]);
            }

            if (IsLowestPoint(map[y][x], adjacent))
            {
                total += map[y][x] + 1;
            }
        }
    }
    return total;
}

int PartTwo(int[][] map)
{
    var basins = new Dictionary<int, List<(int x, int y)>>();
    int basinCount = 0;
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            var pos = (x, y);
            if (map[y][x] < 9 && !basins.Where(x => x.Value.Contains(pos)).Any())
            {
                var result = GetBasin(map, new List<(int x, int y)>() { pos });
                basins.Add(++basinCount, result);
            }
        }
    }

    return Multiply(basins.OrderByDescending(x => x.Value.Count()).Take(3).Select(x => x.Value.Count));
}

bool IsLowestPoint(int point, List<int> adjacent)
{
    if (adjacent.All(x => x > point))
    {
        return true;
    }
    return false;
}

List<(int x, int y)> GetBasin(int[][] map, List<(int x, int y)> basin)
{
    var adjacent = new List<(int x, int y)>();
    foreach (var position in basin)
    {
        if (position.x > 0)
        {
            if (map[position.y][position.x - 1] < 9) adjacent.Add((position.x - 1, position.y));
        }
        if (position.x + 1 < map[position.y].Length)
        {
            if (map[position.y][position.x + 1] < 9) adjacent.Add((position.x + 1, position.y));
        }
        if (position.y > 0)
        {
            if (map[position.y - 1][position.x] < 9) adjacent.Add((position.x, position.y - 1));
        }
        if (position.y + 1 < map.Length)
        {
            if (map[position.y + 1][position.x] < 9) adjacent.Add((position.x, position.y + 1));
        }
    }
    if (adjacent.Except(basin).Count() > 0)
    {
        basin.AddRange(adjacent.Except(basin));
        GetBasin(map, basin);
    }
    return basin;
}

int Multiply(IEnumerable<int> numbers) 
{
    int total = 1;
    foreach(int number in numbers)
    {
        total *= number;
    }
    return total;
}