using System.Linq.Expressions;
using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Repository;


public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected readonly AppDbContext _context;
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

    public DbSet<T> Query()
    {
        return dbset;
    }
    public async Task<PagedResultDto<T>> getAllAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> includeProperties = null)
    {
        IQueryable<T> query = dbset;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            query = includeProperties(query);
        }

        if (pageIndex == 0 && pageSize == 0)
        {
            var items = await query.ToListAsync();
            var total = await query.CountAsync();
            return new PagedResultDto<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items,

            };
        }
        else
        {
            var items = await query.Skip(pageIndex * pageSize)
                      .Take(pageSize)
                      .ToListAsync();
            var total = await query.CountAsync();
            return new PagedResultDto<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items,

            };
        }

    }

    public async Task<T> getByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> includeProperties = null)
    {
        IQueryable<T> query = dbset;

        if (includeProperties != null)
        {
            query = includeProperties(query);
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

    public async Task<List<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await dbset.AddRangeAsync(entities);
        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();
        // Eklenen entiteleri döndür
        return entities.ToList();
    }

    public async Task<T> softDelete(int id)
    {
        var element = await dbset.FirstOrDefaultAsync(item => item.Id == id);
        element.IsDeleted = true;
        return element;
    }
}