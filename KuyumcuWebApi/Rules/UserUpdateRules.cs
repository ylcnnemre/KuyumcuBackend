using KuyumcuWebApi.exception;
using KuyumcuWebApi.Repository;

namespace KuyumcuWebApi.Rules;


public class UserUpdateRules
{
    private readonly IUserRepository userRepository;

    public UserUpdateRules(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task emailControl(string email)
    {
        var user = await userRepository.getByEmail(email);

        if (user is not null)
        {
            throw new ConflictException("bu email zaten mevcut");
        }
    }

    public async Task phoneControl(string phone)
    {
        var user = await userRepository.getByPhone(phone);

        if (user is not null)
        {
            throw new ConflictException("bu telefon zaten mevcut");
        }
    }
}