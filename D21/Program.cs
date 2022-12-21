var testLines = File.ReadLines("./testInput.txt").ToList();
var lines = File.ReadLines("./input.txt").ToList();

var monkeys = ParseInput(lines);
var rootMonkey = monkeys.Find(m => m.Name == "root");
if (rootMonkey == null) throw new ("No root monkey");
var number = GetNumberFromMonkey(rootMonkey);

// Not
// 945240480

Console.WriteLine("Number for root is " + number);

List<Monkey> ParseInput(List<string> input)
{
    var newMonkeys = new List<Monkey>();

    foreach (var line in input)
    {
        var topParts = line.Split(":");
        var jobParts = topParts[1].Split((" "));

        var monkey = new Monkey();
        monkey.Name = topParts[0];

        if (jobParts.Length == 2)
        { 
            // Number monkey
            monkey.Job =int.Parse(jobParts[1]);
            if (monkey.Job == 0) throw new Exception("This is shit");
        }
        else
        {
            // Reference monkey
            monkey.FirstMonkey = jobParts[1];
            monkey.MathOperator = jobParts[2];
            monkey.SecondMonkey = jobParts[3];
        }
        newMonkeys.Add(monkey);
    }

    return newMonkeys;
}

long GetNumberFromMonkey(Monkey monkey)
{
        if (monkey.Job != 0) return monkey.Job;

        var firstMonkey = monkeys.Find(m => m.Name == monkey.FirstMonkey);
        var secondMonkey = monkeys.Find(m => m.Name == monkey.SecondMonkey);

        if (firstMonkey is null || secondMonkey is null) throw new Exception("Missing monkey");
        
        switch (monkey.MathOperator)
        {
            case "+":
                return GetNumberFromMonkey(firstMonkey) + GetNumberFromMonkey(secondMonkey);
            case "-":
                return GetNumberFromMonkey(firstMonkey) - GetNumberFromMonkey(secondMonkey);
            case "*":
                return GetNumberFromMonkey(firstMonkey) * GetNumberFromMonkey(secondMonkey);
            case "/":
                return GetNumberFromMonkey(firstMonkey) / GetNumberFromMonkey(secondMonkey);
            default:
                throw new("This should not happen");
        }
}

class Monkey
{
    public string Name { get; set; }
    public long Job { get; set; } = 0;
    public string FirstMonkey { get; set; }
    public string SecondMonkey { get; set; }
    public string MathOperator { get; set; }
}
