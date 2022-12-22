var testLines = File.ReadLines("./testInput.txt").ToList();
var lines = File.ReadLines("./input.txt").ToList();

var monkeys = ParseInput(lines);

var rootMonkey = monkeys.Find(m => m.Name == "root");
if (rootMonkey is null) throw new ("No root monkey");
var human = monkeys.Find(m => m.Name == "humn");
if (human is null) throw new("Missing human");

human.Job = -234141649530404;

var number = GetNumberFromMonkey(rootMonkey);
var rootFirstMonkey = monkeys.Find(m => m.Name == rootMonkey.FirstMonkey);
var rootSecondMonkey = monkeys.Find(m => m.Name == rootMonkey.SecondMonkey);
// var newNum = GetNumberFromMonkey(rootSecondMonkey);
// human.Job = newNum;

// x + 19843

Console.WriteLine("Expression root");
Console.WriteLine(GetExpressionFromMonkey(rootMonkey));
Console.WriteLine("Expression root first");
Console.WriteLine(GetExpressionFromMonkey(rootFirstMonkey));
Console.WriteLine("Expression root second");
Console.WriteLine(GetExpressionFromMonkey(rootSecondMonkey));
Console.WriteLine("Number root");
Console.WriteLine(GetNumberFromMonkey(rootMonkey));
Console.WriteLine("Number root first");
Console.WriteLine(GetNumberFromMonkey(rootFirstMonkey));
Console.WriteLine("Number root second");
Console.WriteLine(GetNumberFromMonkey(rootSecondMonkey));

// Expression Left side is 247581197056028 + 2 + 30 + 3 + 695 + 394 + 2 + 978 + 11 + 2 + 228 + 641 + 28 + 37 + 940 + 425 + 7 + 15 + x + 173 +  + 3 + 393 + 668 + 2 + 332 + 2 + 725 + 894 + 11 + 829 + 3 + 3 + 988 + 3 + 685 + 2 + 2 + 100 + 2 + 923 + 5 + 6 + 714 + 850 + 3 + 941 + 2 + 672 + 688 + 2 + 185 + 112 + 3 + 383 + 3 + 638 + 454 + 11 + 12 + 157 + 3 + 565 + 940 + 2 + 2 + 310 + 2 + 580 + 2 + 409 + 6
// Expression left shortened is  247581197056028 x + 19843, which equals 247581197075871 + x
// Expression right side is 990619268
// I give up

Console.WriteLine(247581197056028 + 19843);
// Number left is -1
// Number right is 13439547545467

// Final equation is
// 247581197075871 + x = 13439547545467
// or
// x = 13439547545467 - 247581197075871
// which is -234141649530404
Console.WriteLine(13439547545467 - 247581197075871);


// while (number != 1)
// {
//     human.Job += 500000000;
//     Console.Clear();
//     var diff = GetNumberFromMonkey(rootFirstMonkey) - GetNumberFromMonkey(rootSecondMonkey);
//     Console.WriteLine("Diff " + diff);
//     Console.WriteLine("Trying " + human.Job);
//     Console.WriteLine(number);
//     Thread.Sleep(20);
//     // Console.WriteLine(GetNumberFromMonkey(rootFirstMonkey) + " == " + GetNumberFromMonkey(rootSecondMonkey));
// }

// 247581197056028 + x + 19843 = 
// Console.WriteLine("Number for root is " + number);
// Console.WriteLine("Number for human is " + human.Job);

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
            if (monkey.Name == "humn")
            {
                monkey.x = "x";
            }
        }
        else
        {
            // Reference monkey
            monkey.FirstMonkey = jobParts[1];
            monkey.MathOperator = jobParts[2];
            monkey.SecondMonkey = jobParts[3];

            if (monkey.Name == "root") monkey.MathOperator = "=";
        }
        newMonkeys.Add(monkey);
    }

    return newMonkeys;
}

double GetNumberFromMonkey(Monkey monkey)
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
            case "=":
                // Console.WriteLine(GetNumberFromMonkey(firstMonkey) + " == " + GetNumberFromMonkey(secondMonkey));
                var firstMonkeyNum = GetNumberFromMonkey(firstMonkey);
                var secondMonkeyNum = GetNumberFromMonkey(secondMonkey);
                if (firstMonkeyNum == secondMonkeyNum)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
                return firstMonkeyNum == GetNumberFromMonkey(secondMonkey) ? 1 : firstMonkeyNum;
            default:
                throw new("This should not happen");
        }
}

string GetExpressionFromMonkey(Monkey monkey)
{
    if (monkey.Job != 0) return monkey.Job.ToString();
    
    var firstMonkey = monkeys.Find(m => m.Name == monkey.FirstMonkey);
    var secondMonkey = monkeys.Find(m => m.Name == monkey.SecondMonkey);

    if (firstMonkey is null || secondMonkey is null) throw new Exception("Missing monkey");

    if (!string.IsNullOrEmpty(firstMonkey.x))
    {
        return "x + " + GetNumberFromMonkey(secondMonkey) + " + ";
    }

    if (!string.IsNullOrEmpty(secondMonkey.x))
    {
        return GetNumberFromMonkey(secondMonkey) + "+ x + ";
    }

    var expressionLeft = GetExpressionFromMonkey(firstMonkey);
    var expressionRight = GetExpressionFromMonkey(secondMonkey);

    if (expressionLeft.Contains('x'))
    {
        return expressionLeft + " + " + GetNumberFromMonkey(secondMonkey);
    }
    
    if (expressionRight.Contains('x'))
    {
        return GetNumberFromMonkey(firstMonkey) + " + " + expressionRight;
    }

    return "" + (GetNumberFromMonkey(firstMonkey) + GetNumberFromMonkey(secondMonkey)) + "";
};

class Monkey
{
    public string Name { get; set; }
    public double Job { get; set; } = 0;
    public string FirstMonkey { get; set; }
    public string SecondMonkey { get; set; }
    public string MathOperator { get; set; }

    public string x;
}
