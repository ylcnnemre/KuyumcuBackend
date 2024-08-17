using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;
using KuyumcuWebApi.Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Service;

public class UserService
{
    private readonly IUserRepository userRepository;
    private readonly UserUpdateRules userUpdateRules;
    public UserService(IUserRepository userRepository, UserUpdateRules userUpdateRules)
    {
        this.userRepository = userRepository;
        this.userUpdateRules = userUpdateRules;
    }

    public async Task<List<User>> GetAllUserService(PagedRequestDto pagedRequestDto)
    {
        var userList = await userRepository.getAllAsync(pagedRequestDto.PageIndex,pagedRequestDto.PageSize,
        null,               // Filtreleme 
        u => u.Include(x => x.role) 
    );
        var formatList = userList.Items.Select(item => new User
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
        User user = await userRepository.getByIdAsync(userId, u => u.Include(x => x.role));

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


    public async Task<User> updateUser(UpdateUserRequestDto updateUserRequestDto)
    {
        var selectedUser = await userRepository.getByIdAsync(updateUserRequestDto.userId,null);
        if (selectedUser is null)
        {
            throw new NotFoundException("Kullanıcı bulunamadı");
        }
        await userUpdateRules.emailControl(updateUserRequestDto.Email);
        await userUpdateRules.phoneControl(updateUserRequestDto.Phone);


        selectedUser.Phone = updateUserRequestDto.Phone;
        selectedUser.Email = updateUserRequestDto.Email;
        selectedUser.FirstName = updateUserRequestDto.Email;
        selectedUser.LastName = updateUserRequestDto.LastName;
        var updateElement = await userRepository.updateAsync(selectedUser);

        var User = new User()
        {
            Id = updateElement.Id,
            FirstName = updateElement.FirstName,
            LastName = updateElement.LastName,
            Phone = updateElement.Phone,
            Email = updateElement.Email,
            role = new Role(){
                Id = selectedUser.role.Id,
                Name = selectedUser.role.Name
            },
            RoleId = updateElement.RoleId,
            isActive = updateElement.isActive
        };

        return User;
    }


    public async Task<User> updateUserStatus(UserStatusRequestDto userStatusRequestDto)
    {
        var selectedUser = await userRepository.getByIdAsync(userStatusRequestDto.UserId,null);
        if (selectedUser is null)
        {
            throw new NotFoundException("Kullanıcı bulunamadı");
        }

        selectedUser.isActive = userStatusRequestDto.isActive;
        var updateUser= await userRepository.updateAsync(selectedUser);
        var newUser = new User(){
            Id = updateUser.Id, 
            FirstName = updateUser.FirstName,
            LastName = updateUser.LastName,
            Email = updateUser.Email,
            isActive= updateUser.isActive,
            Phone = updateUser.Phone,
            role = new Role(){
                Id = selectedUser.role.Id,
                Name = selectedUser.role.Name
            },
            RoleId = updateUser.RoleId
        };
        return newUser;
    }

}