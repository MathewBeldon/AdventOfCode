using Shared;

const int ROUND_COUNT = 10000;

var arr = Helper.ParseToStringArray(11).Select(x => x.Trim()).ToArray();
var monkeys = new List<Monkey>();

bool isP1 = ROUND_COUNT == 20;

string[] parseList = { "Monkey", "Starting", "Operation", "Test", "If true", "If false" };
//generate the monkeys
for (int i = 0; i < arr.Length; i++)
{
    var monkey = new Monkey();
    for (; i < arr.Length; i++)
    {
        if (string.IsNullOrWhiteSpace(arr[i]))
        {
            break;
        }
        switch (parseList.First(s => arr[i].StartsWith(s)))
        {
            case "Monkey":
                monkey.Id = long.Parse(arr[i].Substring(7, 1));
                continue;
            case "Starting":
                monkey.Items = arr[i].Substring(16).Split(',').Select(long.Parse).ToList();
                continue;
            case "Operation":
                monkey.FnOperation = ParseOperation(arr[i].Substring(21));
                continue;
            case "Test":
                monkey.Divider = int.Parse(arr[i].Substring(19));
                continue;
            case "If true":
                monkey.TrueMonkeyId = int.Parse(arr[i].Substring(25));
                continue;
            case "If false":
                monkey.FalseMonkeyId = int.Parse(arr[i].Substring(26));
                continue;
        }
    }
    monkeys.Add(monkey);
}

var LCM = monkeys.Select(m => m.Divider).Aggregate((a, b) => a * b);

//run the game
for (int round = 0; round < ROUND_COUNT; round++)
{
    foreach (var monkey in monkeys)
    {
        var trueList = new List<long>();
        var falseList = new List<long>();

        for (int item = 0; item < monkey.Items.Count; item++)
        {
            monkey.inspectCount++;
            monkey.Items[item] = monkey.FnOperation(monkey.Items[item]) % LCM;
            if (monkey.Items[item] % monkey.Divider == 0)
            {
                trueList.Add(monkey.Items[item]);
            }
            else
            {
                falseList.Add(monkey.Items[item]);
            }
        }
        monkey.Items = monkey.Items.Except(trueList).Except(falseList).ToList();
        monkeys.Single(m => m.Id == monkey.FalseMonkeyId).Items.AddRange(falseList);
        monkeys.Single(m => m.Id == monkey.TrueMonkeyId).Items.AddRange(trueList);
    }
}

long total = monkeys.OrderByDescending(x => x.inspectCount).Take(2).Select(m => m.inspectCount).Aggregate((a, b) => a * b);
Console.WriteLine($"Part {(isP1 ? "One" : "Two" )} {total}");
Console.ReadLine();

Func<long, long> ParseOperation(string operation)
{
    switch (operation)
    {
        case var s when s.Contains("*"):
            return Parse((x, y) => x * y);
        case var s when s.Contains("+"):
            return Parse((x, y) => x + y);
        default:
            throw new NotImplementedException();
    }

    Func<long, long> Parse(Func<long, long, long> func)
    {
        if (operation.Contains("old"))
        {
            return (x) => (long)Math.Floor((decimal)func(x, x) / (isP1 ? 3 : 1));
        }
        return (x) => (long)Math.Floor((decimal)func(x, long.Parse(operation.Substring(2))) / (isP1 ? 3 : 1));
    }
}

class Monkey
{
    public long Id { get; set; }
    public List<long> Items { get; set; }
    public Func<long, long> FnOperation { get; set; }
    public int Divider { get; set; }
    public int TrueMonkeyId { get; set; }
    public int FalseMonkeyId { get; set; }
    public long inspectCount { get; set; }
}