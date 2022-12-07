var lines = File.ReadLines("input.txt").ToList();

var tree = new Stack<Directory>();
var rootDir = new Directory { Name = "/" };
tree.Push(rootDir);

for (var i = 1; i < lines.Count; i++)
{
    var line = lines[i];
    var currentCommand = line.Split(" ")[1];
    switch (currentCommand)
    {
        case "cd":
        {
            var targetDirectory = line.Split(" ")[2];
            ChangeDirectory(targetDirectory);
            break;
        }
        case "ls":
            break;
        default:
        {
            var currentDir = tree.Peek();
            if (!line.StartsWith("dir")) // Is directory
            {
                var fileName = line.Split(" ")[1];
                if (currentDir.Files.FirstOrDefault(f => f.Name == fileName) == null)
                {
                    var fileSize = int.Parse(line.Split(" ")[0]);
                    var newFile = new MyFile { Size = fileSize, Name = fileName };
                    currentDir.Files.Add(newFile);
                }
                else
                {
                    throw new Exception("oops");
                }
            }
            break;
        }
    }
}

var part1 = 0;
GetSums(rootDir);
Console.WriteLine("Sum of dirs with size up to 100000: " + part1);

var spaceToFreeUp = 30000000 - (70000000 - rootDir.Size);
var dirToDelete = rootDir;
GetDirToDelete(rootDir);
Console.WriteLine("Dir to delete " + dirToDelete.Name + " has size: " + dirToDelete.Size);

void GetDirToDelete(Directory directory)
{
    if (directory.Size >= spaceToFreeUp && directory.Size <= dirToDelete.Size)
    {
        dirToDelete = directory;
    }

    foreach (var dir in directory.Directories)
    {
        GetDirToDelete(dir);
    }
}

void GetSums(Directory dir)
{
    if (dir.Size <= 100000)
    {
        part1 += dir.Size;
    }
    foreach (var subDir in dir.Directories)
    {
        GetSums(subDir);
    }
};

void ChangeDirectory(string targetDirectory)
{
    switch (targetDirectory)
    {
        case "/":
            return;
        case "..":
            tree.Pop();
            return;
        default:
            var currentDir = tree.Peek();
            var nextDir = currentDir.Directories.FirstOrDefault(d => d.Name == targetDirectory) ?? 
                          new Directory { Name = targetDirectory };
            tree.Push(nextDir);
            currentDir.Directories.Add(nextDir);
            break;
    }
}

internal class Directory
{
    public string Name { get; init; } = "";
    public List<Directory> Directories { get; } = new ();
    public List<MyFile> Files { get; } = new ();
    public int Size => Files.Sum(f => f.Size) + Directories.Sum(d => d.Size);
}

internal record MyFile(string Name = "", int Size = 0);