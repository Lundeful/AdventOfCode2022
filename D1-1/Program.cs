var caloriesPerElf = new List<int>();
var currentElfCalorieCount = 0;
var maxValue = 0;
var maxValueIndex = 0;
foreach (var line in File.ReadLines("./ElfCaloriesList.txt"))
{
    if (string.IsNullOrEmpty(line))
    {
        caloriesPerElf.Add(currentElfCalorieCount);
        if (currentElfCalorieCount > maxValue)
        {
            maxValue = currentElfCalorieCount;
            maxValueIndex = caloriesPerElf.Count-1;
        }
        currentElfCalorieCount = 0;
    }
    else
    {
        currentElfCalorieCount += int.Parse(line); 
    }
}

Console.WriteLine($"Elf number {maxValueIndex + 1} has the most calories with {caloriesPerElf[maxValueIndex]} calories");

caloriesPerElf.Sort();
caloriesPerElf.Reverse();
Console.WriteLine($"The top three elves are carrying a total of {caloriesPerElf[0] + caloriesPerElf[1] + caloriesPerElf[2]} calories");