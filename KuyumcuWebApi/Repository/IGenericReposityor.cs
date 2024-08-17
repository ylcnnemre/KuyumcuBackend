using System.Linq.Expressions;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<T> getByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> includeProperties);
    Task<PagedResultDto<T>> getAllAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> includeProperties = null);
    void Delete(T entity);
    Task<T> updateAsync(T entity);
    Task<T> addAsync(T entity);
    Task<int> saveChangesAsync();

    Task<T> softDelete(int id);
    Task<List<T>> AddRangeAsync(IEnumerable<T> entities);

}