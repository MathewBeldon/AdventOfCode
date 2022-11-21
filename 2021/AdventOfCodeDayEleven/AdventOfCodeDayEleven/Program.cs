var lines = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayEleven\input.txt").ToArray();

int[][] map = new int[lines.Length][];

for (int y = 0; y < lines.Length; y++)
{
    map[y] = new int[lines[y].Length];
    for (int x = 0; x < lines[y].Length; x++)
    {
        map[y][x] = int.Parse(lines[y][x].ToString());
    }
}

int[][] deepClone = map.Select(x => x.ToArray()).ToArray();

var checkPoints = new point[]
{
    new point { x = -1, y = 0 },
    new point { x = -1, y = -1 },
    new point { x = 0, y = -1 },
    new point { x = 1, y = -1 },
    new point { x = 1, y = 0 },
    new point { x = 1, y = 1 },
    new point { x = 0, y = 1 },
    new point { x = -1, y = 1 }
};


int partOneResult = PartOne(map, 100);
int partTwoResult = PartTwo(deepClone);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(int[][] map, int steps)
{
    int flashCount = 0;

    for (int i = 0; i < steps; i++)
    {
        List<(int x, int y)> marked = new List<(int x, int y)>();
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 9)
                {
                    var result = GetFlashes(map, new List<(int x, int y)> { (x, y) }, marked);
                    map = result.map;
                    marked = result.marked;
                }
                map[y][x]++;
            }
        }
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] > 9)
                {
                    map[y][x] = 0;
                    flashCount++;
                }
            }
        }
    }

    return flashCount;
}

int PartTwo(int[][] map)
{
    int syncFlash = 0;
    int counter = 0;

    while (syncFlash == 0)
    {
        int flashCount = 0;

        List<(int x, int y)> marked = new List<(int x, int y)>();
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 9)
                {
                    var result = GetFlashes(map, new List<(int x, int y)> { (x, y) }, marked);
                    map = result.map;
                    marked = result.marked;
                }
                map[y][x]++;
            }
        }
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] > 9)
                {
                    map[y][x] = 0;
                    flashCount++;
                }
            }
        }
        ++counter;
        if (flashCount >= 100)
        {
            syncFlash = counter;
        }
    }

    return syncFlash;
}

(int[][] map, List<(int x, int y)> marked) GetFlashes(int[][] map, List<(int x, int y)> flashes, List<(int x, int y)> marked)
{
    var adjacent = new List<(int x, int y)>();
    foreach (var f in flashes.Except(marked))
    {
        foreach(var pnt in checkPoints)
        {
            if (f.x + pnt.x >= 0 && 
                f.y + pnt.y >= 0 &&
                f.x + pnt.x < map[f.y].Length &&
                f.y + pnt.y < map.Length )
            {
                map[f.y + pnt.y][f.x + pnt.x]++;
                //Console.WriteLine("Curent " + f + "changing " + (f.y + pnt.y) + " " + (f.x + pnt.x));
                if (map[f.y + pnt.y][f.x + pnt.x] > 9)
                {
                    adjacent.Add((f.x + pnt.x, f.y + pnt.y));
                }
            }
        }
    }

    marked.AddRange(flashes.Except(marked));    
    if (adjacent.Count > 0)
    {
        GetFlashes(map, adjacent, marked);
    }

    return (map, marked);
}

void PrintMap(int[][] map)
{
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            Console.Write(map[y][x] > 9 ? 0 : map[y][x]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

struct point
{
    public int x;
    public int y;
}