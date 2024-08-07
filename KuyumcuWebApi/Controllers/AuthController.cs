using KuyumcuWebApi.dto;
using KuyumcuWebApi.Security;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IConfiguration configuration;
    private readonly AuthService authService;
    public AuthController(IConfiguration configuration, AuthService authService)
    {
        this.configuration = configuration;
        this.authService = authService;
    }

    /* [HttpPost]
    public IActionResult Index([FromBody] UserLoginModel userLogin)
    {
        Token token = TokenHandler.CreateToken(configuration);

        return Ok(token);
    } */

    /* [HttpGet("[Action]")]
    [Authorize]
    public IActionResult Secure()
    {

        return Ok(new
        {
            message = "selam1x"
        });
    }

    [HttpGet("[Action]")]
    [Authorize(Roles = "User")]
    public IActionResult RoleAuth()
    {
        return Ok(new
        {
            message = "çalışıyor"
        });
    } */

    [HttpPost("[Action]")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var result = await authService.Login(userLoginDto);

        return Ok(result);
    }



    [HttpPost("[Action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await authService.Register(registerDto);

        return Ok(new SuccessRegisterDto()
        {
            message = "Kullanıcı kaydedildi",
            data = result
        });
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roleList = await authService.getAllRoles();
        return Ok(new SuccessResponseDto()
        {
            message = "Success",
            data = roleList
        });
    }
}