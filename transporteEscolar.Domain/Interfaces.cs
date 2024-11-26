using transporteEscolar.Domain;


public interface GetDataInterface<R>
{
    R Get(int id);
    IEnumerable<R> All();
    void Add(R element);
    void Delete(int id);
    void Update(R element);

}

