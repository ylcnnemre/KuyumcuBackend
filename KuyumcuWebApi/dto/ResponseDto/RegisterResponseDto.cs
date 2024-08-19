using KuyumcuWebApi.Models;
using KuyumcuWebApi.Security;

namespace KuyumcuWebApi.dto;


public class RegisterResponseDto : Token
{
    public User User { get; set; }
}