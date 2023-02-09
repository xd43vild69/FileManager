namespace Repositories;
public interface IRepository<T>
    {
        IEnumerable<T> GetLists();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }