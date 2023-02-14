using DTO;

namespace Repositories;

public abstract class AbstractRepository<T>
{

    public readonly IConfiguration Configuration;
    public readonly string? ConnectionString;
    const string db_name  = "dbTest2023";

    public AbstractRepository(IConfiguration configuration)
    {
        Configuration = configuration;
        ConnectionString = Configuration.GetConnectionString(db_name);
    }

    public abstract void Insert(T entity);

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetLists()
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

}