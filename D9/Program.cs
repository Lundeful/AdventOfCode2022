using System.Threading.Tasks.Dataflow;

var lines = File.ReadLines("input.txt");

const int amountOfKnots = 10; // 2 for part 1, 10 for part 2
var knots = new List<(int x, int y)>();
for (var i = 0; i < amountOfKnots; i++)
{
    knots.Add((0,0));
}

var visitedTailCoordinates = new HashSet<(int x, int y)> { (0, 0) };
var history = new Stack<List<(int x, int y)>>();
history.Push(knots);

foreach (var line in lines)
{
    var parts = line.Split(" ");
    var direction = parts[0];
    var amount = int.Parse(parts[1]);

    for (var i = 0; i < amount; i++)
    {
        MoveHead(direction);
        for (var knotIndex = 0; knotIndex < knots.Count - 1; knotIndex++)
        {
            UpdateFollowingKnot(knotIndex, knotIndex + 1);
        }
        
        history.Push(new(knots.ToArray()));
    }
}

Console.WriteLine("Positions visited at least once: " + visitedTailCoordinates.Count);
PrintConsoleArt();

void MoveHead(string direction)
{
    var head = knots[0];
    switch (direction)
    {
        case "U":
            head.y++;
            break;
        case "R":
            head.x++;
            break;
        case "D":
            head.y--;
            break;
        case "L":
            head.x--;
            break;
        default:
            throw new ArgumentException("Not valid direction");
    }

    knots[0] = head;
}

void UpdateFollowingKnot(int headIndex, int tailIndex)
{
    var head = knots[headIndex];
    var tail = knots[tailIndex];

    var xDiff = head.x - tail.x;
    var yDiff = head.y - tail.y;

    if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1)
    {
        // Knots are touching, don't move
        return;
    }
    
    // Should ever only move one tile at a time
    xDiff = Math.Clamp(xDiff, -1, 1);
    yDiff = Math.Clamp(yDiff, -1, 1);

    tail.x += xDiff;
    tail.y += yDiff;
    
    knots[tailIndex] = tail;

    if (tailIndex == knots.Count - 1)
    {
        visitedTailCoordinates.Add((tail.x, tail.y));   
    }
}

void PrintConsoleArt()
{
    var minX = history.Min(l => l.Min(t => t.x));
    var maxX = history.Max(l => l.Max(t => t.x));
    var minY = history.Min(l => l.Min(t => t.y));
    var maxY = history.Max(l => l.Max(t => t.y));

    var width = maxX - minX;
    var height = maxY - minY;
    var grid = new string[width, height];

    Console.CursorVisible = false;

    // Loop over history
    foreach (var list in history)
    {
        // Fill it with blanks and frame
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                var content = " ";
                if (i == 0 || i == width-1)
                {
                    content = "_";
                } else if (j == 0 || j == height-1)
                {
                    content = "|";
                }
                grid[i, j] = content;
            }
        }
        
        // Fill in the rope values
        foreach (var t in list)
        {
            grid[t.x + width/2, t.y + height/2] = "@";
        }

        var s = "";
        // Print the grid
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                s += grid[i, j];
            }

            s += "\n";
        }

        Console.SetCursorPosition(0,0);
        Console.Clear();
        Console.WriteLine(s);
        Thread.Sleep(500);
    }
}