var input = File.ReadLines("input.txt").ToList();
// input = File.ReadLines("testinput.txt").ToList();

var grid = new Node[input.Count, input[0].Length];
Node startNode = new Node();
Node endNode = new Node();
var possibleStartingNodes = new List<Node>();

ParseInput();
var result = BFS();
Console.WriteLine("Amount of steps: " + (result));

foreach (var node in grid)
{
    if (node.Value == 1)
    {
        ParseInput();
        startNode.Distance = -1;
        node.Distance = 0;
        startNode = node;
        var newRes = BFS();
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

int BFS()
{
    var nodes = new Queue<Node>();
    nodes.Enqueue(startNode);
    while (nodes.Any())
    {
        // PrintGrid();
        var node = nodes.Dequeue();
        node.Visited = true;
        if (node == endNode)
        {
            Console.WriteLine("FOOUND IT: " + node.Distance);
            return node.Distance;
        }
        var adjacentNodes = GetValidNodes(node);
        foreach (var adjacentNode in adjacentNodes)
        {
            if (adjacentNode.Visited) continue;

            adjacentNode.Visited = true;
            // if (adjacentNode.Distance >= node.Distance + 1 || adjacentNode.Distance == -1)
            // {
                adjacentNode.Distance = node.Distance + 1;
                nodes.Enqueue(adjacentNode);
            // }
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

List<Node> GetValidNodes(Node node)
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
