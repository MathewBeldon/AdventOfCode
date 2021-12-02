using System.Globalization;

string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayTwo\input.txt").ToArray();

int partOneResult = PartOne(arr);
int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");


int PartOne(string[] arr)
{
    int hp = 0;
    int fd = 0;

    for (int i = 0; i < arr.Length; i++)
    {
        switch (arr[i][0])
        {
            case 'f':
                hp += int.Parse(arr[i][arr[i].Length -1].ToString());
                break;
            case 'd':
                fd += int.Parse(arr[i][arr[i].Length - 1].ToString());
                break;
            case 'u':
                fd -= int.Parse(arr[i][arr[i].Length - 1].ToString());
                break;
        }
    }
    return hp * fd;
}

int PartTwo(string[] arr)
{
    int hp = 0;
    int fd = 0;
    int aim = 0;

    for (int i = 0; i < arr.Length; i++)
    {
        switch (arr[i][0])
        {
            case 'f':
                int forwardMove = int.Parse(arr[i][arr[i].Length - 1].ToString());
                hp += forwardMove;
                fd += forwardMove * aim;
                break;
            case 'd':
                aim += int.Parse(arr[i][arr[i].Length - 1].ToString());
                break;
            case 'u':
                aim -= int.Parse(arr[i][arr[i].Length - 1].ToString());
                break;
        }
    }
    return hp * fd;
}
