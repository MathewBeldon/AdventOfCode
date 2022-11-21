string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayFour\input.txt").Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
const int GRID_SIZE = 5;

var res = ParseInput(arr);
int partOneResult = PartOne(res.calls, res.pages);
int partTwoResult = PartTwo(res.calls, res.pages);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(int[] calls, int[][][] pages)
{
    bool[,,] hits = new bool[pages.Length, GRID_SIZE, GRID_SIZE];
    for (int call = 0; call < calls.Length; call++)
    {
        for (int page = 0; page < pages.Length; page++)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    if (calls[call] == pages[page][row][column])
                    {
                        hits[page, row, column] = true;
                        if (BingoChecker(hits, page, row, column))
                        {
                            return ScoreCalculator(hits, page, pages[page], pages[page][row][column]);
                        }
                    }
                }
            }
        }
    }
    return 0;
}

int PartTwo(int[] calls, int[][][] pages)
{
    bool[,,] hits = new bool[pages.Length, GRID_SIZE, GRID_SIZE];
    List<int> winners = new List<int>();
    for (int call = 0; call < calls.Length; call++)
    {
        for (int page = 0; page < pages.Length; page++)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    if (calls[call] == pages[page][row][column])
                    {
                        hits[page, row, column] = true;
                        if (BingoChecker(hits, page, row, column))
                        {
                            if (!winners.Contains(page)) 
                            {
                                winners.Add(page);
                                if (winners.Count == pages.Length)
                                {
                                    return ScoreCalculator(hits, page, pages[page], pages[page][row][column]);
                                }
                            }         
                        }
                    }
                }
            }
        }
    }
    return 0;
}

bool BingoChecker(bool[,,] hits, int page, int row, int column)
{
    int columnCounter = 0;
    int rowCounter = 0;
    for (int i = 0; i < GRID_SIZE; i++)
    {
        if (hits[page, i, column]) columnCounter++;
        if (hits[page, row, i]) rowCounter++;
    }
    return rowCounter == 5 || columnCounter == 5;
}

int ScoreCalculator(bool[,,] hits, int pageNumber, int[][] page, int winningNumber)
{
    int total = 0;
    for (int row = 0; row < page.Length; row++)
    {
        for (int column = 0; column < page[row].Length; column++)
        {
            if (!hits[pageNumber, row, column])
            {
                total += page[row][column];
            }
        }
    }

    return total * winningNumber;
}

(int[] calls, int[][][] pages) ParseInput(string[] arr)
{
    int[] calls = arr[0].Split(',').Select(s => int.Parse(s)).ToArray();

    int numberOfPages = (arr.Length - 1) / GRID_SIZE;

    int[][][] pages = new int[numberOfPages][][];

    for (int i = 0; i < numberOfPages; i++)
    {
        pages[i] = new int[GRID_SIZE][];
        for (int j = 0; j < GRID_SIZE; j++)
        {
            pages[i][j] = arr[(i * GRID_SIZE) + j + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
        }
    }

    return (calls, pages);
}