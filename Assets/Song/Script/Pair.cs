public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T x, U y)
    {
        this.x = x;
        this.y = y;
    }

    public T x { get; set; }
    public U y { get; set; }
};
