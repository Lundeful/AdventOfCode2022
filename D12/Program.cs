var input = File.ReadLines("input.txt").ToList();
// input = File.ReadLines("testinput.txt").ToList(); // Test input, should be 31 for Part 1, 29 for Part 2

var grid = new Node[input.Count, input[0].Length];
var startNode = new Node();
var endNode = new Node();

ParseInput();
var result = FindShortestPath(startNode);
Console.WriteLine("Amount of steps from S: " + (result));

foreach (var node in grid)
{
    if (node.Value == 1)
    {
        ResetGrid();
        node.Distance = 0;
        var newRes = FindShortestPath(node);
        result = newRes > 0 ? Math.Min(newRes, result) : result;
    }
}

Console.WriteLine("Shortest starting point: " + result);

void ParseInput()
{
    for (var rowIndex = 0; rowIndex < input.Count; rowIndex++)
    {
        var line = input[rowIndex];
        for (var colIndex = 0; colIndex < line.Length; colIndex++)
        {
            var letter = line[colIndex];
            
            var node = new Node
            {
                Row = rowIndex,
                Col = colIndex,
                Visited = false,
                Value = letter.ToString().ToLower()[0]-96,
                Distance = -1
            };
            
            if (letter == 'S')
            {
                letter = 'a';
                startNode = node;
                node.Value = letter.ToString().ToLower()[0] - 96;
                node.Distance = 0;
            }
            else if (letter == 'E')
            {
                letter = 'z';
                node.Value = letter.ToString().ToLower()[0] - 96;
                endNode = node;
                node.Distance = -1;
            }
            grid[rowIndex, colIndex] = node;
        }
    }
}

// Breadth First Search
int FindShortestPath(Node firstNode)
{
    var nodes = new Queue<Node>();
    nodes.Enqueue(firstNode);
    while (nodes.Any())
    {
        // PrintGrid();
        var node = nodes.Dequeue();
        node.Visited = true;
        if (node == endNode)
        {
            // Console.WriteLine("FOOUND IT: " + node.Distance);
            return node.Distance;
        }
        var adjacentNodes = GetValidNodes(node);
        foreach (var adjacentNode in adjacentNodes.Where(adjacentNode => !adjacentNode.Visited))
        {
            adjacentNode.Visited = true;
            adjacentNode.Distance = node.Distance + 1;
            nodes.Enqueue(adjacentNode);
        }
    }

    return endNode.Distance;
}

void PrintGrid()
{
    var content = "";
    for (var i = 0; i < grid.GetLength(0); i++)
    {
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            var dist = grid[i, j].Distance;
            content += dist is >= 10 or < 0 ? dist : "0" + dist;
            content += " ";
        }
        content += "\n";
    }
    Console.WriteLine(content);
}

void ResetGrid()
{
    foreach (var node in grid)
    {
        node.Visited = false;
        node.Distance = -1;
    }
}

IEnumerable<Node> GetValidNodes(Node node)
{
    var row = node.Row;
    var col = node.Col;

    var canGoUp = row > 0 && grid[row - 1, col].Value <= node.Value + 1;
    var canGoDown = row + 1 < grid.GetLength(0) && grid[row + 1, col].Value <= node.Value + 1;
    var canGoLeft = col > 0 && grid[row, col - 1].Value <= node.Value + 1;
    var canGoRight = col + 1 < grid.GetLength(1) && grid[row, col + 1].Value <= node.Value + 1;

    var availableNodes = new List<Node>();
    
    if (canGoUp)
        availableNodes.Add(grid[row - 1, col]);

    if (canGoDown) 
        availableNodes.Add(grid[row + 1, col]);

    if (canGoLeft)
        availableNodes.Add(grid[row, col - 1]);

    if (canGoRight)
        availableNodes.Add(grid[row, col + 1]);
    
    return availableNodes;
}
