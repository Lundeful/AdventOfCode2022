namespace D11;

public class MonkeyComparer : IComparer<Monkey>
{
    public int Compare(Monkey? x, Monkey? y)
    {
        if (y != null && x != null && x.ItemsInspected > y.ItemsInspected)
        {
            return -1;
        }

        if (y != null && x != null && x.ItemsInspected < y.ItemsInspected)
        {
            return 1;
        }

        return 0;
    }
}