using KuyumcuWebApi.Context;
using KuyumcuWebApi.Models;

namespace KuyumcuWebApi.Repository;

public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context) : base(context)
    {
    }
    
    
}