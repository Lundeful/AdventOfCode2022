public class Monkey
{
    public int Id { get; set; }
    public List<int> StartingItems { get; set; }
    public string Operator { get; set; }
    public string Operand { get; set; }
    public int Test { get; set; }
    public int TrueDestination { get; set; }
    public int FalseDestination { get; set; }

    public int ItemsInspected { get; set; } = 0;
}