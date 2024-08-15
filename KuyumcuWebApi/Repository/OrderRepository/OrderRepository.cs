using KuyumcuWebApi.Context;
using KuyumcuWebApi.Models;

namespace KuyumcuWebApi.Repository;


public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    
}