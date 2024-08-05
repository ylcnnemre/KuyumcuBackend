using KuyumcuWebApi.dto;
using KuyumcuWebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase {

    private readonly IConfiguration configuration;

    public AuthController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult Index([FromBody] UserLoginModel userLogin) {
        Token token=  TokenHandler.CreateToken(configuration);
        
        return Ok(token);
    } 

    [HttpGet("[Action]")]
    [Authorize]
    public IActionResult Secure(){

        return Ok(new {
            message = "selam1x"
        });
    }
}