using Shared;

var arr = Helper.ParseToStringArray(6).First();

var previous = new List<char>();
var counter = 0;
foreach(char c in arr)
{
    counter++;
    previous.Add(c);
    if (previous.Count < 4)
    {
        continue;
    }
    if (previous.Distinct().Count() == previous.Count)
    {
        Console.WriteLine(counter);
        break;
    }
    previous = previous.Skip(1).ToList();
}

var previous2 = new List<char>();
var counter2 = 0;
foreach (char c in arr)
{
    counter2++;
    previous2.Add(c);
    if (previous2.Count < 14)
    {
        continue;
    }
    if (previous2.Distinct().Count() == previous2.Count)
    {
        Console.WriteLine(counter2);
        break;
    }
    previous2 = previous2.Skip(1).ToList();
}