using transporteEscolar.Domain;


public interface DataInterface<R>
{
    Task<R> Get(int id);
    Task<IEnumerable<R>> All();
    Task Add(R element);
    Task Delete(int id);
    Task Update(R element);

}

public interface BillsInterface<R>
{
    Task Update(Bills bill);
    Task Add(int studentId);
    Task<R> Get(int id);
    Task Delete(int StudentId);
}

