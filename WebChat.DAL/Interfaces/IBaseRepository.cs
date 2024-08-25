namespace WebChat.DAL.Interfaces;

public interface IBaseRepository<T>
{
  Task<T?> Get(string id);
  Task<List<T>> GetAll();
  Task Create(T entity);
  Task Update(string id, T entity);
  Task Delete(string id);
}