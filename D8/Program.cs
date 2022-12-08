var lines = File.ReadLines("input.txt").ToList();
// lines = File.ReadLines("testinput.txt").ToList(); // Verify it's working with test values

var gridSize = lines.Count;
var grid = new int[lines.Count][];

for (var i = 0; i < lines.Count; i++) {
    grid[i] = lines[i].Select(c => int.Parse(c.ToString())).ToArray();
}

var amountOfTreesVisible = 0;
var scenicTopScore = 0;

for (var i = 0; i < grid.Length; i++)
{
    var row = grid[i];
    for (var j = 0; j < row.Length; j++)
    {
        if (IsTreeVisible(i, j, grid[i][j]))
        {
            amountOfTreesVisible++;
        }

        var scenicScore = GetScenicScore(i, j, grid[i][j]);
        if (scenicScore > scenicTopScore)
        {
            scenicTopScore = scenicScore;
        }
    }
}

bool IsTreeVisible(int row, int col, int value)
{
    // Check edge
    if (row == 0 || col == 0 || row == gridSize-1 || col == gridSize-1)
    {
        return true;
    }
    
    var valuesToTheLeft = grid[row].Take(col);
    if (valuesToTheLeft.All(val => val < value))
    {
        return true;
    }
    
    var valuesToTheRight = grid[row].Skip(col+1);
    if (valuesToTheRight.All(val => val < value))
    {
        return true;
    }

    var valuesToTheTop = grid.Select(r => r[col]).Take(row);
    if (valuesToTheTop.All(val => val < value))
    {
        return true;
    }

    var valuesToTheBottom = grid.Select(r => r[col]).Skip(row+1);
    return valuesToTheBottom.All(val => val < value);
}

int GetScenicScore(int row, int col, int value)
{
    // Check edge
    if (row == 0 || col == 0 || row == gridSize-1 || col == gridSize-1)
    {
        return 0;
    }
    
    var valuesToTheLeft = new Stack<int>(grid[row].Take(col));
    var leftSideScore = 0;
    while (valuesToTheLeft.Any())
    {
        var nextValue = valuesToTheLeft.Pop();
        leftSideScore++;
        if (nextValue >= value)
        {
            break;
        }
    }
    
    
    // Check right side
    var valuesToTheRight = new Stack<int>(grid[row].Skip(col+1).Reverse());
    var rightSideScore = 0;
    while (valuesToTheRight.Any())
    {
        var nextValue = valuesToTheRight.Pop();
        rightSideScore++;

        if (nextValue >= value)
        {
            break;
        }
    }
    
    // Check top side
    var valuesToTheTop = new Stack<int>(grid.Select(r => r[col]).Take(row));
    var topSideScore = 0;
    while (valuesToTheTop.Any())
    {
        var nextValue = valuesToTheTop.Pop();
        topSideScore++;

        if (nextValue >= value)
        {
            break;
        }
    }
    
    // Check bottom side
    var valuesToTheBottom = new Stack<int>(grid.Select(r => r[col]).Skip(row+1).Reverse());
    var bottomSideScore = 0;
    while (valuesToTheBottom.Any())
    {
        var nextValue = valuesToTheBottom.Pop();
        bottomSideScore++;

        if (nextValue >= value)
        {
            break;
        }
    }

    return leftSideScore * rightSideScore * topSideScore * bottomSideScore;
}

Console.WriteLine("Amounts of trees visible: " + amountOfTreesVisible);
Console.WriteLine("Highest scenic score: " + scenicTopScore);