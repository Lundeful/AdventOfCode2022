var input = File.ReadLines("input.txt").First();
const int numOfUniqueChars = 14;
var result = input.TakeWhile((c, i) => i <= numOfUniqueChars || input.Skip(i-numOfUniqueChars).Take(numOfUniqueChars).Distinct().Count() < numOfUniqueChars).Count();
Console.WriteLine(result);
