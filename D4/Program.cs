using Range = D4.Range;

var lines = File.ReadLines("./input.txt")
    .Select(l => l.Replace("-", ".."));

// Create a list to hold the tuples
var tuples = new List<Tuple<Range, Range>>();

// Loop through each line
foreach (var line in lines)
{
    // Split the line into two parts, separated by a comma
    var parts = line.Split(',');

    // Try to parse the first part into a range
    if (Range.TryParse(parts[0], out var first))
    {
        // If the parse was successful, try to parse the second part
        if (Range.TryParse(parts[1], out var second))
        {
            // If the parse was successful, add a new tuple to the list with the two ranges
            tuples.Add(new Tuple<Range, Range>(first, second));
        }
    }
}

// Counter for pairs in which one range fully contains the other
var counter = 0;

// Part 1
foreach (var tuple in tuples)
{
    // Check if one range completely contains the other
    if (tuple.Item1.Start <= tuple.Item2.Start && tuple.Item2.End <= tuple.Item1.End)
    {
        counter++;
    }
    else if (tuple.Item2.Start <= tuple.Item1.Start && tuple.Item1.End <= tuple.Item2.End)
    {
        counter++;
    }
}

// Part 2
// Counter for pairs that overlap at all
var counter2 = tuples.Count(tuple => tuple.Item1.Start <= tuple.Item2.End && tuple.Item2.Start <= tuple.Item1.End);

// Print the result
Console.WriteLine(counter);
Console.WriteLine(counter2);