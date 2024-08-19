using KuyumcuWebApi.Models;
using KuyumcuWebApi.Security;

namespace KuyumcuWebApi.dto;


public class LoginResponseDto : Token
{
    public User User { get; set; }  

}