var caloriesPerElf = new List<int>();
var currentElfCalorieCount = 0;
foreach (var line in File.ReadLines("./ElfCaloriesList.txt"))
{
    if (string.IsNullOrEmpty(line))
    {
        caloriesPerElf.Add(currentElfCalorieCount);
        currentElfCalorieCount = 0;
    }
    else
    {
        currentElfCalorieCount += int.Parse(line); 
    }
}
caloriesPerElf.Sort();
caloriesPerElf.Reverse();
Console.WriteLine($"The top elf has food worth {caloriesPerElf.First()} calories");
Console.WriteLine($"The top three elves are carrying a total of {caloriesPerElf[0] + caloriesPerElf[1] + caloriesPerElf[2]} calories");