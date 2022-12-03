// Part 1
int GetPointForLetter(char letter)
{
    var baseValue = letter == char.ToLower(letter) ? 0 : 26;
    var letterValue = char.ToUpper(letter) - 64;
    var totalValue = baseValue + letterValue;
    return totalValue;
}

var rucksacks = File.ReadLines("./input.txt").ToList();
var itemsThatAppearInBothRucksacks = new List<char>();

foreach (var rucksack in rucksacks)
{
    var firstCompartment = rucksack.Take(rucksack.Length / 2).ToList();
    var secondCompartment = rucksack.Skip(rucksack.Length / 2).ToList();
    itemsThatAppearInBothRucksacks.AddRange(firstCompartment.Intersect(secondCompartment).ToList());
}

var sumOfPriorities = itemsThatAppearInBothRucksacks.Sum(GetPointForLetter);
Console.WriteLine("Sum of priority for duplicated items: " + sumOfPriorities);

// Part 2
var badgeLetters = rucksacks
    .Select((x, i) => new { Index = i, Value = x })
    .GroupBy(x => x.Index / 3)
    .Select(x => x.Select(v => v.Value).ToList())
    .ToList()
    .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).ToList().First());

var sumOfBadges = badgeLetters.Sum(GetPointForLetter);
Console.WriteLine("Sum of badge priorities: " + sumOfBadges);