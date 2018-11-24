namespace Books.Core
{
    public interface IBooksSource
    {
        Book[] Read();
        void Delete(Book book);
        void Add(Book book);
    }
}
