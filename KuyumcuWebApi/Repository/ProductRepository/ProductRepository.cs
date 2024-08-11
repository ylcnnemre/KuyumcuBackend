using KuyumcuWebApi.Context;
using KuyumcuWebApi.Models;

namespace KuyumcuWebApi.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
        
    }
}