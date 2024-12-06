public interface DataInterface<R>
{
    R Get(int id);
    IEnumerable<R> All();
    Result Add(R element);
    Result Delete(int id);
    Result Update(R element);

}