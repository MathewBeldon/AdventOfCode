using Shared;

var arr = Helper.ParseToStringArray(7)
    .Select(x => x.Replace("$ ", string.Empty))
    .Where(x => !x.StartsWith("dir"))
    .Where(x => !x.StartsWith("ls"))
    .Where(x => !string.IsNullOrWhiteSpace(x));

var cwd = new Stack<Folder>();
var root = new Folder();
foreach(var item in arr)
{
    if (item.StartsWith("cd"))
    {
        if (item.Contains(".."))
        {
            cwd.Pop();
        }
        else
        {
            var folder = new Folder
            {
                Name = item.Replace("cd ", string.Empty),
                Files = new List<File>(),
                SubFolders = new List<Folder>()
            };

            if (string.IsNullOrWhiteSpace(root.Name))
            {                
                root = folder;
                cwd.Push(folder);
                continue;
            }
            
            cwd.Peek().SubFolders.Add(folder);
            cwd.Push(folder);
        }
        continue;
    }
    cwd.Peek().Files.Add(new File
    {
        Name = item.Split(" ")[1],
        Size = int.Parse(item.Split(" ")[0]),
    });
}

long total = 0;
GetFolderSizeUnder100k(root, ref total);

List<long> total2 = new List<long>();
GetSmallestFolderToDelete(root, 70000000 - root.Size(), total2);

Console.WriteLine($"Part One: {total}");
Console.WriteLine($"Part Two: {total2.Min()}");

Console.ReadLine();

void GetFolderSizeUnder100k(Folder folder, ref long total)
{
    if (folder.Size() <= 100000)
    {
        total += folder.Size();
    }
    foreach(var item in folder.SubFolders)
    {
        GetFolderSizeUnder100k(item, ref total);
    }
}

void GetSmallestFolderToDelete(Folder folder, long startingSize, List<long> total)
{
    if (startingSize + folder.Size() > 30000000)
    {
        total.Add(folder.Size());
    }
    foreach (var item in folder.SubFolders)
    {
        GetSmallestFolderToDelete(item, startingSize, total);
    }
}

class Folder
{
    public string Name { get; set; }
    public List<File> Files { get; set; }
    public List<Folder> SubFolders { get; set; }

    public long Size()
    {
        long size = 0;
        size += Files.Select(x => x.Size).Sum();
        size += SubFolders.Select(x => x.Size()).Sum();
        return size;
    }
}

class File
{
    public string Name { get; set; }
    public long Size { get; set; }
}