public interface ServicesInterface<R>
{
    Task<R> Get(int id);
    Task<IEnumerable<R>> All();
    Task<Result> Add(R element);
    Task<Result> Delete(int id);
    Task<Result> Update(R element, int id);

}

public interface PaysInterface<R>
{
    Task<IEnumerable<R>> All(int studentId);
    Task<Result> Add(R element);
}