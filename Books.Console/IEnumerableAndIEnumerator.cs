namespace Books.Examples
{

    public interface IEnumerable<out T>
    {
        IEnumerator<T> GetEnumerator();
    }
    
    public interface IEnumerator<out T>
    {
        T Current { get; }

        bool MoveNext();

        void Reset();
    }
    
}
