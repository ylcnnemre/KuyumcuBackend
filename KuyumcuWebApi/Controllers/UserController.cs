using System.Security.Claims;
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

    [HttpPost("[Action]")]
    public async Task<IActionResult> GetAllUser([FromBody] PagedRequestDto pagedRequestDto )
    {
        ICollection<User> userlist = await this.userService.GetAllUserService(pagedRequestDto);

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

    [HttpPut("[Action]")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        User updateUser = await userService.updateUser(updateUserRequestDto);

        return Ok(new SuccessResponseDto()
        {
            message = "Kullanıcı güncellendi",
            data = updateUser
        });
    }

    [HttpPut("[Action]")]

    public async Task<IActionResult> UpdateUserStatus([FromBody] UserStatusRequestDto userStatusRequestDto)
    {
        var result = await userService.updateUserStatus(userStatusRequestDto);
        return Ok(new SuccessResponseDto()
        {
            message = "Kullanıcı durumu güncellendi",
            data = result
        });
    }

    [HttpPut("[Action]")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = User.FindFirst("id").Value;
        var res = User.Claims.FirstOrDefault(item => item.Type == "lastName")?.Value;
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        return Ok(new
        {
            token = token,
            data = res
        });
    }
}