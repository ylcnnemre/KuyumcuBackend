using KuyumcuWebApi.Models;

namespace KuyumcuWebApi.Repository;


public interface IUserRepository:IGenericRepository<User> {

    public Task<User> getByEmail(string email); 
    public Task<User> getByPhone(string phone);
}
