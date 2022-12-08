var lines = File.ReadLines("./input.txt")
    .Select(l => l.Replace("-", ".."));

var tuples = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

foreach (var line in lines)
{
    var parts = line.Split(',');

    var first = new Tuple<int, int>(int.Parse(parts[0].Split("-")[0]),
        int.Parse(parts[0].Split("-")[1]));
    
    var second = new Tuple<int, int>(int.Parse(parts[1].Split("-")[0]),
        int.Parse(parts[1].Split("-")[1]));

    tuples.Add(new (first, second));
}

// Counter for pairs in which one range fully contains the other
var counter = 0;

// Part 1
foreach (var tuple in tuples)
{
    // Check if one range completely contains the other
    if (tuple.Item1.Item1 <= tuple.Item2.Item1 && tuple.Item2.Item2 <= tuple.Item1.Item2)
    {
        counter++;
    }
    else if (tuple.Item2.Item1 <= tuple.Item1.Item1 && tuple.Item1.Item2 <= tuple.Item2.Item2)
    {
        counter++;
    }
}

// Part 2
// Counter for pairs that overlap at all
var counter2 = tuples.Count(tuple => tuple.Item1.Item1 <= tuple.Item2.Item2 && tuple.Item2.Item1 <= tuple.Item1.Item2);

// Print the result
Console.WriteLine(counter);
Console.WriteLine(counter2);