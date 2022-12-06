var input = File.ReadLines("input.txt").First();
const int numberOfUniqueCharacters = 14;
var itemsBeforeMarker = FindMarker(input, numberOfUniqueCharacters);
Console.WriteLine("Processed characters: " + (itemsBeforeMarker-1));

static bool IsListUnique<T>(IReadOnlyCollection<T> list) => list.Distinct().Count() == list.Count;

static int FindMarker(string text, int numberOfUniqueCharacters)
{
    var lastFour = text.Take(numberOfUniqueCharacters).ToList();

    for (var i = 4; i < text.Length; i++)
    {
        if (IsListUnique(lastFour))
            return i + 1;

        lastFour = lastFour.Skip(1).ToList();
        lastFour.Add(text[i]);
    }

    return -1;
}

