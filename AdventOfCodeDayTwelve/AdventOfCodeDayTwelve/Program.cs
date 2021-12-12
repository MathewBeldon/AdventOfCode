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
    CaveRoute tree = new CaveRoute("start");
    foreach (var route in routes)
    {
        var currentCave = tree.PercolateFind(route[0]);
        currentCave.AddChild(route[1]);
        Console.WriteLine("");
        //if ()
    }

    return 0;
}

int PartTwo(int[] arr)
{
   
    return 0;
}

void MapRoute(ref CaveRoute caveRoute, string cave, List<string[]> routes)
{
    foreach (var route in routes.Where(x => x[0] == cave || x[1] == cave))
    {
        var alt = route[0] == cave ? route[1] : route[0];
        if (caveRoute.AddChild(cave) is not null)
        {
            MapRoute(ref caveRoute, alt, routes);
        }
    }
}

public class CaveRoute
{
    public string Cave { get; private set; }
    public bool BigCave { get; private set; }
    public CaveRoute Parent { get; private set; }
    public List<CaveRoute> Children { get; private set; }

    public CaveRoute(string cave)
    {
        Cave = cave;
        Children = new List<CaveRoute>();
    }

    public CaveRoute AddChild(string cave)
    {

        CaveRoute parent = this;
        var route = new CaveRoute(cave) { Parent = parent, BigCave = cave.ToLower() != cave };
        if (route.BigCave)
        {
            Children.Add(route);
            return route;
        }
        if (PercolateFind(cave) is null)
        {
            Children.Add(route);
            return route;
        }
        return null;
    }

    public CaveRoute PercolateFind(string cave)
    {
        CaveRoute currentCave = this;
        if (currentCave.Cave == cave)
        {
            return currentCave;
        }
        while (currentCave.Parent is not null)
        {
            if (currentCave.Cave == cave)
            {
                return currentCave;
            }
            currentCave = currentCave.Parent;
        }
        return null;
    }
}