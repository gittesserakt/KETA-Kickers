namespace Edgar.Graphs
{
    public interface IEdge<T>
    {
        T From { get; }
        T To { get; }
    }
}
