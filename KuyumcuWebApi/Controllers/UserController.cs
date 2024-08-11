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
        return Ok(new SuccessResponseDto(){
            message = "Kullanıcı durumu güncellendi",
            data = result
        });
    }

}