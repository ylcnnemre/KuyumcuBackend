using KuyumcuWebApi.Context;
using KuyumcuWebApi.Models;

namespace KuyumcuWebApi.Repository;

public class UserRepository : GenericRepository<User>,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    


}