var stackLines = File.ReadLines("stacks.txt");
var instructionLines = File.ReadLines("./input.txt").Skip(10);
var stacks = stackLines.Select(stackLine => 
    stackLine
        .ToCharArray()
        .Select(c => c.ToString()).ToList()).ToList();

var instructions = new List<Tuple<int, int, int>>();

foreach (var line in instructionLines)
{
    var parts = line.Split(' ');
    var numCrates = int.Parse(parts[1]);
    var sourceStack = int.Parse(parts[3][..1]);
    var destStack = int.Parse(parts[5][..1]);
    instructions.Add(Tuple.Create(numCrates, sourceStack, destStack));
}

// PART 1
// foreach (var (numCrates, item2, item3) in instructions)
// {
//     var sourceStack = item2 - 1; // -1 because stacks are 0-indexed
//     var destStack = item3 - 1;
//
//     for (var i = 0; i < numCrates; i++)
//     {
//         var crate = stacks[sourceStack][stacks[sourceStack].Count - 1];
//         stacks[sourceStack].RemoveAt(stacks[sourceStack].Count - 1);
//         stacks[destStack].Add(crate);
//     }
// }

//  PART 2
foreach (var (numCrates, item2, item3) in instructions)
{
    var sourceStack = item2 - 1; // -1 because stacks are 0-indexed
    var destStack = item3 - 1;

    var stackTobeMoved = new List<string>();

    for (var i = 0; i < numCrates; i++)
    {
        var crate = stacks[sourceStack][stacks[sourceStack].Count - 1];
        stacks[sourceStack].RemoveAt(stacks[sourceStack].Count - 1);
        stackTobeMoved.Add(crate);
    }
    
    stackTobeMoved.Reverse();
    stacks[destStack].AddRange(stackTobeMoved);
}

foreach (var crate in stacks)
{
    Console.Write(crate[^1]);
}

