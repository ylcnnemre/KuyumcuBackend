using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Rules;


public class RegisterRules
{

    private readonly AppDbContext appDbContext;
    public RegisterRules(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    public async Task uniqueUserControl(RegisterDto registerDto)
    {
       await emailControl(registerDto.Email);
    }

    private async Task emailControl(string email)
    {
        var result = await appDbContext.users.AnyAsync(item => item.Email == email);
        if (result)
        {
            throw new ConflictException("bu email zaten mevcut");
        }
    }

    

}