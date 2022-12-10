using Shared;

var arr = Helper.ParseToStringArray(9, true).Select(x => x.Split(" ")).Select(x => (move: x[0], distance: int.Parse(x[1].ToString()))).ToArray();

var head = new Coordinate();

var visitedP1 = new HashSet<(int x, int y)>() { (0, 0) };
var visitedP2 = new HashSet<(int x, int y)>() { (0, 0) };

var rope = new Coordinate[9].Select(x => new Coordinate()).ToArray();

foreach (var moves in arr)
{
    //default to UP
    Func<Coordinate, int> fnMove = (item) => ++item.y;
    int pos = head.y;
    switch (moves.move)
    {
        case "L":
            fnMove = (item) => --item.x;
            break;
        case "D":
            fnMove = (item) => --item.y;
            break;
        case "R":
            fnMove = (item) => ++item.x;
            break;
        default:
            break;
    }

    for (int move = 0; move < moves.distance; move++)
    {
        fnMove(head);
        for (int i = 0; i < rope.Length; i++)
        {
            UpdatePosition(i == 0 ? head : rope[i - 1], rope[i]);
        }
        visitedP1.Add((rope[0].x, rope[0].y));
        visitedP2.Add((rope[8].x, rope[8].y));
    }

    PrintGrid(head, rope);
}

void UpdatePosition(Coordinate head, Coordinate tail)
{
    if (Math.Abs(head.x - tail.x) + Math.Abs(head.y - tail.y) > 2)
    {
        // U1 R1
        if (head.x > tail.x && head.y > tail.y)
        {
            tail.x++;
            tail.y++;
            return;
        }
        // U1 L1
        if (head.x < tail.x && head.y > tail.y)
        {
            tail.x--;
            tail.y++;
            return;
        }
        // D1 L1
        if (head.x < tail.x && head.y < tail.y)
        {
            tail.x--;
            tail.y--;
            return;
        }
        // D1 R1
        if (head.x > tail.x && head.y < tail.y)
        {
            tail.x++;
            tail.y--;
            return;
        }
    }

    if (!(Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1))
    {
        // U1
        if (head.x - tail.x == 0 && head.y - tail.y > 1)
        {
            tail.y++;
            return;
        }
        // L1
        if (head.x - tail.x < -1 && head.y - tail.y == 0)
        {
            tail.x--;
            return;
        }
        // D1
        if (head.x - tail.x == 0 && head.y - tail.y < -1)
        {
            tail.y--;
            return;
        }
        // R1
        if (head.x - tail.x > 1 && head.y - tail.y == 0)
        {
            tail.x++;
            return;
        }
    }
    return;
}

Console.WriteLine($"Part One: {visitedP1.Count()}");
Console.WriteLine($"Part Two: {visitedP2.Count()}");

Console.ReadLine();

void PrintGrid(Coordinate head, Coordinate[] rope, bool large = true)
{
    for (int i = (large ? 15 : 5); i > (large ? -15 : 0); i--)
    {
        for (int j = (large ? -15 : 0); j < (large ? 15 : 6); j++)
        {
            var write = string.Empty;
            for (int x = 0; x < rope.Length; x++)
            {
                if (rope[x].x == j && rope[x].y == i)
                {
                    write = x.ToString();
                }
            }

            if (i == 0 && j == 0)
            {
                Console.Write("s");
            }
            else if (head.x == j && head.y == i)
            {
                Console.Write("H");
            }
            else if (!string.IsNullOrWhiteSpace(write))
            {
                Console.Write(write);
            }
            else
            {
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

class Coordinate
{
    public int x;
    public int y;

    public override string ToString()
    {
        return $"{x},{y}";
    }
}