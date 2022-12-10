using System.Collections;

var lines = File.ReadLines("input.txt").ToList();
// lines = File.ReadLines("testInput.txt").ToList();

var cycle = 0;
var totalSignalStrength = 0;
var registerValue = 1;

foreach (var line in lines)
{
    var parts = line.Split(" ");
    if (parts[0] == "addx")
    {
        cycle++;
        AddSignalStrength();
        cycle++;
        AddSignalStrength();
        registerValue += int.Parse(parts[1]);
    }
    else
    {
        cycle++;
        AddSignalStrength();
    }
}

void AddSignalStrength()
{
    if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
    {
        Console.WriteLine(" Cycle:  "  + cycle + ". Register value: " + registerValue + ". Total: " + registerValue * cycle);
        totalSignalStrength += (registerValue * cycle);   
    }
}


Console.WriteLine("Total signal strength: " + totalSignalStrength);