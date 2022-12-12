class Node
{
    public int Row { get; init; }
    public int Col { get; init; }
    public int Value { get; set; }
    public bool Visited { get; set; }
    public int Distance { get; set; }
}