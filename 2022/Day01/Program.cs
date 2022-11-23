using Shared;

var arr = Helper<int>.ParseRangeToStringArray(1, 5, int.MaxValue);

Console.WriteLine(arr.Length);
Console.ReadLine();