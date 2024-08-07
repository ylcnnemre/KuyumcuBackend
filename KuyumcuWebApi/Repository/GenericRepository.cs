using System.Linq.Expressions;
using KuyumcuWebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Repository;


public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> dbset;

    public GenericRepository(AppDbContext context)
    {
        this._context = context;
        dbset = _context.Set<T>();
    }

    public async Task<T> addAsync(T entity)
    {
        var entityEntry = await dbset.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public void Delete(T entity)
    {
        dbset.Remove(entity);
    }

    public async Task<List<T>> getAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = dbset;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public async Task<T> getByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = dbset;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.SingleOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }
    public async Task<int> saveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public async Task<T> updateAsync(T entity)
    {
        var entityEntry = dbset.Update(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }
}