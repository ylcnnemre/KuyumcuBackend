using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Rules;
using KuyumcuWebApi.Security;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Service;

public class AuthService
{
    AppDbContext appContext;
    RegisterRules registerRules;
    IConfiguration configuration;


    public AuthService(AppDbContext context, RegisterRules rules, IConfiguration configuration)
    {
        this.appContext = context;
        this.registerRules = rules;
        this.configuration = configuration;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public bool VerifyPassword(string hashedPassword, string passwordToVerify)
    {
        return BCrypt.Net.BCrypt.Verify(passwordToVerify, hashedPassword);
    }


    public async Task<User> Register(RegisterDto registerDto)
    {
        await registerRules.uniqueUserControl(registerDto);

        var roleInfo = await appContext.roles.FirstOrDefaultAsync(x => x.Id == registerDto.RoleId);
        var createdUser = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = HashPassword(registerDto.Password),
            RoleId = registerDto.RoleId,
            Phone = registerDto.Phone,
            isActive = true,
            role = roleInfo
        };
        appContext.users.Add(createdUser);
        await appContext.SaveChangesAsync();
        createdUser.Password=null;
        return createdUser;
    }


    public async Task<LoginResponseDto> Login(UserLoginDto loginDto)
    {
        var selectedUser = await appContext.users.Include(el => el.role).FirstOrDefaultAsync(item => item.Email == loginDto.email);
        /* Token token = TokenHandler.CreateToken(configuration); */
        if (selectedUser is null)
        {
            throw new UnauthorizedException("Böyle bir kullanıcı bulunamadı");
        }
        bool passwordMatch = VerifyPassword(selectedUser.Password, loginDto.password);
        if (!passwordMatch)
        {
            throw new UnauthorizedException("Şifre yanlış");
        }
        Token token = TokenHandler.CreateToken(configuration, selectedUser);
        selectedUser.Password = null;
        return new LoginResponseDto()
        {
            AccessToken = token.AccessToken,
            Expiration = token.Expiration,
            User = selectedUser,
        };
    }

    public async Task<List<Role>> getAllRoles()
    {
        var list = await appContext.roles.ToListAsync();
        return list;
    }

}