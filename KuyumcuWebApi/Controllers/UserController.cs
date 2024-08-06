using KuyumcuWebApi.dto;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : Controller
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetAllUser()
    {
        ICollection<User> userlist = await this.userService.GetAllUserService();

        return Ok(new SuccessResponseDto()
        {
            message = "başarılı",
            data = userlist
        });
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var user = await this.userService.getById(userId);
        
        return Ok(new SuccessResponseDto()
        {
            message = "başarılı",
            data = user
        });
    }
}