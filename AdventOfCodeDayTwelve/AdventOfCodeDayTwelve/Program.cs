var routes = File.ReadLines(@"C:\src\AOC\AdventOfCodeDayTwelve\test.txt").Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries)).ToList();

//for (int i = 0; i < routes.Count; i++)
//{
//    if (routes[i][0] != routes[i][0].ToLower())
//    {
//        routes.Add(new string[] { routes[i][1], routes[i][0] });
//    }
//}

int partOneResult = PartOne(routes);
//int partTwoResult = PartTwo(arr);

Console.WriteLine($"Part One: {partOneResult} increases");
//Console.WriteLine($"Part Two: {partTwoResult} increases");

int PartOne(List<string[]> routes)
{
    Routes tree = new Routes("start");
    for (int i = 0; i < routes.Count; i++)
    {
        for (int j = 0; j + i < routes.Count; j++)
        {
            AddRoute(tree, (routes[j + i][0], routes[j + i][1]));
        }
    }
    foreach (var route in routes)
    {
        AddRoute(tree, (route[0], route[1]));
        //if ()
    }

    return 0;
}

int PartTwo(int[] arr)
{
   
    return 0;
}


void AddRoute(Routes route, (string lhs, string rhs) connection)
{
    Add(connection.lhs, connection.rhs);
    Add(connection.rhs, connection.lhs);

    if (route.Children is not null)
    {
        foreach(var child in route.Children)
        {
            AddRoute(child, connection);
        }
    }

    void Add(string parent, string child)
    {
        if (parent == route.Cave)
        {
            if (!SearchUp(route, child))
            {
                if (!route.Children.Any(x => x.Cave == child))
                {
                    route.Children.Add(new Routes(child) { Parent = route, BigCave = child.ToLower() != child });
                }
            }
        }
    }
}

bool SearchUp(Routes route, string cave)
{
    while (route is not null)
    {
        if (route.Cave == "end")
        {
            return true;
        }
        if (route.Cave == cave)
        {
            if (!route.BigCave)
            {
                return true;
            }
        }
        route = route.Parent;
    }
    return false;
}

public class Routes
{
    public string Cave { get; set; }
    public bool BigCave { get; set; }
    public Routes Parent { get; set; }
    public List<Routes> Children { get; set; }

    public Routes(string cave)
    {
        Cave = cave;
        Children = new List<Routes>();
    }
}


