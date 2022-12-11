var lines = File.ReadLines("input.txt").ToList();
// lines = File.ReadLines("testInput.txt").ToList();

var cycle = 0;
var totalSignalStrength = 0;
var registerValue = 1;
var pixelPos = 0;

foreach (var line in lines)
{
    Tick();
    var parts = line.Split(" ");
    if (parts[0] == "addx")
    {
        Tick();
        registerValue += int.Parse(parts[1]);
    }
}

void Tick()
{
    cycle++;
    CheckSignalStrength();
    CalculatePixels();
}

void CalculatePixels()
{
    if (registerValue == pixelPos || registerValue == pixelPos - 1 || registerValue == pixelPos + 1)
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(".");
    }
    pixelPos++;
    if (cycle % 40 == 0)
    {
        Console.WriteLine();
        pixelPos = 0;
    }
}

void CheckSignalStrength()
{
    if (cycle is 20 or 60 or 100 or 140 or 180 or 220) totalSignalStrength += (registerValue * cycle);
}

Console.WriteLine("Total signal strength: " + totalSignalStrength); // Part 1