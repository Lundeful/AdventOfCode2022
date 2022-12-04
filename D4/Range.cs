namespace D4;

public class Range
{
    // The start and end of the range
    public int Start { get; private init; }
    public int End { get; private init; }

    // A method to parse a string into a range
    public static bool TryParse(string input, out Range range)
    {
        // Split the input string into start and end parts, separated by two dots
        var parts = input.Split("..");

        // Try to parse the start and end parts into integers
        if (!int.TryParse(parts[0], out var start) || !int.TryParse(parts[1], out var end))
            throw new Exception("Failed to parse");
        
        // If the parse was successful, create a new range object and set the start and end
        range = new Range
        {
            Start = start,
            End = end
        };
        return true;
    }
}