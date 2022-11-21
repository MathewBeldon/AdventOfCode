string[] arr = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayThree\input.txt").ToArray();

int partOneResult = PartOne(arr);
int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult}");
Console.WriteLine($"Part Two: {partTwoResult}");

int PartOne(string[] arr)
{
    char[] result = new char[arr[0].Length];

    for (int i = 0; i < arr[i].Length; i++)
    {
        var countedBits = BitCounter(arr, i);
        result[i] = (countedBits.one > countedBits.zero ? '1' : '0');
    }

    int gamma = Convert.ToInt16(new string(result), 2);
    int epsilon = ~gamma &(short)Math.Pow(2, (arr[0].Length)) - 1;
    return gamma * epsilon;
}

int PartTwo(string[] arr)
{
    int counter = 0;
    var oxygenArr = arr;
    while (oxygenArr.Length > 1)
    {
        var countedBits = BitCounter(oxygenArr, counter);
        oxygenArr = oxygenArr.Where(x => x[counter] == (countedBits.one >= countedBits.zero ? '1' : '0')).ToArray();
        counter++;
    }
    var oxygen = Convert.ToInt16(oxygenArr[0], 2);

    counter = 0;
    var carbonArr = arr;
    while (carbonArr.Length > 1)
    {
        var countedBits = BitCounter(carbonArr, counter);
        carbonArr = carbonArr.Where(x => x[counter] == (countedBits.one >= countedBits.zero ? '0' : '1')).ToArray();
        counter++;
    }
    var carbon = Convert.ToInt16(carbonArr[0], 2);
    
    return  oxygen * carbon;
}

(int zero, int one) BitCounter(string[] arr, int next)
{
    int zero = 0;
    int one = 0;

    for (int i = 0; i < arr.Length; i++)
    {
        switch (arr[i][next])
        {
            case '0':
                zero++;
                break;
            case '1':
                one++;
                break;            
        }
    }
    return (zero, one);
}