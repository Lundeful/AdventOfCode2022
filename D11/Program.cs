using D11;

var lines = File.ReadLines("input.txt").ToList();
// lines = File.ReadLines("testinput.txt").ToList();

var monkeys = ParseInput(lines);

PlayTheFuckingGame();
monkeys.Sort(new MonkeyComparer());
Console.WriteLine("Monkey Business Part 1: " + monkeys[0].ItemsInspected * monkeys[1].ItemsInspected);

void PlayTheFuckingGame()
{
    for (var i = 0; i < 20; i++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var monkeyStartingItem in monkey.StartingItems)
            {
                var myWorryLevel = monkeyStartingItem;

                // Inspect item and apply worry level
                var operand = monkey.Operand == "old" ? myWorryLevel : int.Parse(monkey.Operand);
                if (monkey.Operator == "*")
                    myWorryLevel *= operand;
                else if (monkey.Operator == "+") myWorryLevel += operand;

                // Apply relief
                myWorryLevel /= 3;

                // Apply test and throw
                var testPassed = myWorryLevel % monkey.Test == 0;
                var targetMonkey = monkeys.Find(m =>
                    m.Id == (testPassed ? monkey.TrueDestination : monkey.FalseDestination));
                targetMonkey?.StartingItems.Add(myWorryLevel);

                monkey.ItemsInspected++;
            }

            monkey.StartingItems = new();
        }
    }
}


List<Monkey> ParseInput(IReadOnlyList<string> input)
{
    var monkeyList = new List<Monkey>();

    for (var i = 0; i < input.Count;)
    {
        var id = int.Parse(input[i].Split(" ")[1].Split(":")[0]);
        i++;
        
        var startingItems = input[i].Replace("  Starting items: ", "").Split(",").Select(int.Parse).ToList();
        i++;
        
        var operationType = input[i].Replace("  Operation: new = old ", "").Split(" ")[0];
        var operationNumber = input[i].Replace("  Operation: new = old ", "").Split(" ")[1];
        i++;
        
        var test = int.Parse(input[i].Split(" ").Last());
        i++;
        
        var trueDest = int.Parse(input[i].Split(" ").Last());
        i++;
        
        var falseDest = int.Parse(input[i].Split(" ").Last());
        i++;

        var monkey = new Monkey
        {
            Id = id,
            StartingItems = startingItems,
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