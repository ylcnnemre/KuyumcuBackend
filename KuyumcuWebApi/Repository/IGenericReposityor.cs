using System.Linq.Expressions;

namespace KuyumcuWebApi.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<T> getByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
    Task<List<T>> getAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
    void Delete(T entity);
    Task<T> updateAsync(T entity);
    Task<T> addAsync(T entity);
    Task<int> saveChangesAsync();


}