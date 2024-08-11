using KuyumcuWebApi.Context;
using KuyumcuWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> getByEmail(string email)
    {
        var element = await base._context.users.FirstOrDefaultAsync(item => item.Email == email);
        return element;
    }

    public async Task<User> getByPhone(string phone)
    {
        var element = await base._context.users.FirstOrDefaultAsync(item => item.Phone == phone);
        return element;
    }
}