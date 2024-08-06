using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Service;

public class UserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUserService()
    {
        var userList = await userRepository.getAllAsync(
        null,               // Filtreleme 
        u => u.role
    );
        var formatList = userList.Select(item => new User
        {
            Email = item.Email,
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.LastName,
            role = new Role()
            {
                Id = item.role.Id,
                Name = item.role.Name
            },   // Sadece rolü getiriyoruz, rolün içindeki kullanıcılar gelmeyecek
            RoleId = item.RoleId
        }).ToList();

        return formatList;
    }

    public async Task<User> getById(int userId)
    {
        User user = await userRepository.getByIdAsync(userId, u => u.role);

        if (user is null)
        {
            throw new NotFoundException("Kullanıcı bulunamadı");
        }
        User formatUser = new User()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            RoleId = user.RoleId,
            role = new Role()
            {
                Id = user.role.Id,
                Name = user.role.Name
            }

        };
        return formatUser;
    }
}