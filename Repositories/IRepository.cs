namespace Repositories;
public interface IRepository<T>
    {
        IEnumerable<T> GetLists();
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }