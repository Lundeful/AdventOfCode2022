using System.Numerics;

namespace D11;

public class Monkey
{
    public long Id { get; set; }
    public List<long> Items { get; set; }
    public string Operator { get; set; }
    public string Operand { get; set; }
    public long Test { get; set; }
    public long TrueDestination { get; set; }
    public long FalseDestination { get; set; }

    public long ItemsInspected { get; set; } = 0;
}