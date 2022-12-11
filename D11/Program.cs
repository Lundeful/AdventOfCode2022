using System.Numerics;
using D11;

// var part1 = PlayTheFuckingGame(20);
// Console.WriteLine("Monkey Business Part 1: " + part1);

var part2 = PlayTheFuckingGame(10000);
Console.WriteLine("Monkey Business Part 2: " + part2);

long PlayTheFuckingGame(long rounds)
{
    var lines = File.ReadLines("input.txt").ToList();
    // lines = File.ReadLines("testinput.txt").ToList();

    var monkeys = ParseInput(lines);
    long mod = monkeys.Select(m => m.Test).Aggregate((a, b) => a * b);

    for (var i = 0; i < rounds; i++)
    {
        if (i is (1 or 20) || i % 1000 == 0)
        {
            var a = monkeys[0].ItemsInspected;
            var b = monkeys[1].ItemsInspected;
            var c = monkeys[2].ItemsInspected;
            var d = monkeys[3].ItemsInspected;
            var e = 2;
        }

        foreach (var monkey in monkeys)
        {
            foreach (var item in monkey.Items)
            {
                // var myWorryLevel = item;  // Part 1
                var myWorryLevel = item % mod == 0 ? item : item % mod; // Apply relief for part 2 
                
                // Inspect item and apply worry level
                var operand = monkey.Operand == "old" ? myWorryLevel : long.Parse(monkey.Operand);

                switch (monkey.Operator)
                {
                    case "*":
                        myWorryLevel *= operand;
                        break;
                    case "+":
                        myWorryLevel += operand;
                        break;
                }

                // Apply relief for part 1
                // myWorryLevel /= 3;

                // Apply test and throw
                var testPassed = myWorryLevel % monkey.Test == 0;
                var targetMonkey = monkeys.Find(m =>
                    m.Id == (testPassed ? monkey.TrueDestination : monkey.FalseDestination));
                targetMonkey?.Items.Add(myWorryLevel);
            }

            monkey.ItemsInspected += monkey.Items.Count;
            monkey.Items = new();
        }
    }

    monkeys.Sort(new MonkeyComparer());
    var first = monkeys[0].ItemsInspected;
    var second = monkeys[1].ItemsInspected;
    return first * second;
}

List<Monkey> ParseInput(IReadOnlyList<string> input)
{
    var monkeyList = new List<Monkey>();

    for (var i = 0; i < input.Count;)
    {
        var id = long.Parse(input[i].Split(" ")[1].Split(":")[0]);
        i++;
        
        var startingItems = input[i].Replace("  Starting items: ", "").Split(",").Select(long.Parse).ToList();
        i++;
        
        var operationType = input[i].Replace("  Operation: new = old ", "").Split(" ")[0];
        var operationNumber = input[i].Replace("  Operation: new = old ", "").Split(" ")[1];
        i++;
        
        var test = long.Parse(input[i].Split(" ").Last());
        i++;
        
        var trueDest = long.Parse(input[i].Split(" ").Last());
        i++;
        
        var falseDest = long.Parse(input[i].Split(" ").Last());
        i++;

        var monkey = new Monkey
        {
            Id = id,
            Items = startingItems,
            Operator = operationType,
            Operand = operationNumber,
            Test = test,
            TrueDestination = trueDest,
            FalseDestination = falseDest
        };

        monkeyList.Add(monkey);
        i++;
    }

    return monkeyList;
}