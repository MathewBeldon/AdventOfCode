string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayFive\input.txt").ToArray();

const int GRID_SIZE = 1000;

int partOneResult = PartOne(arr);
int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(string[] arr)
{
    int[,] grid = new int[GRID_SIZE, GRID_SIZE];
    for (int line = 0; line < arr.Length; line++)
    {
        var points = arr[line].Split("->", StringSplitOptions.RemoveEmptyEntries);

        var pointA = points[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
        var pointB = points[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

        var x1 = int.Parse(pointA[0]);
        var y1 = int.Parse(pointA[1]);

        var x2 = int.Parse(pointB[0]);
        var y2 = int.Parse(pointB[1]);

        if (y1 == y2)
        {
            int bigX = x1 > x2 ? x1 : x2;
            int smallX = x1 < x2 ? x1 : x2;

            for (int x = smallX; x <= bigX; x++)
            {
                grid[x, y1]++;

            }
        }

        if (x1 == x2)
        {
            int bigY = y1 > y2 ? y1 : y2;
            int smallY = y1 < y2 ? y1 : y2;

            for (int y = smallY; y <= bigY; y++)
            {
                grid[x1, y]++;
            }
        }
    }
    return GridCounter(grid);
}

int PartTwo(string[] arr)
{
    int[,] grid = new int[GRID_SIZE, GRID_SIZE];
    for (int line = 0; line < arr.Length; line++)
    {
        var points = arr[line].Split("->", StringSplitOptions.RemoveEmptyEntries);

        var pointA = points[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
        var pointB = points[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

        var x1 = int.Parse(pointA[0]);
        var y1 = int.Parse(pointA[1]);

        var x2 = int.Parse(pointB[0]);
        var y2 = int.Parse(pointB[1]);

        int bigX = x1 > x2 ? x1 : x2;
        int smallX = x1 < x2 ? x1 : x2;

        int bigY = y1 > y2 ? y1 : y2;
        int smallY = y1 < y2 ? y1 : y2;

        int range = bigX - smallX > bigY - smallY ? bigX - smallX : bigY - smallY;

        int tmpx = x1;
        int tmpy = y1;

        for (int i = 0; i < range + 1; i++)
        {            
            grid[tmpx, tmpy]++;
            if (x1 > x2)
            {
                tmpx--;
            } 
            else if (x1 < x2)
            {
                tmpx++;
            }

            if (y1 > y2)
            {
                tmpy--;
            }
            else if (y1 < y2)
            {
                tmpy++;
            }
        }
        
    }    
    return GridCounter(grid);
}

int GridCounter(int[,] grid)
{
    int counter = 0;
    for (int x = 0; x < GRID_SIZE; x++)
    {
        for (int y = 0; y < GRID_SIZE; y++)
        {
            if (grid[y,x] > 1) counter++;
        }
        
    }
    return counter;
}