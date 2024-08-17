using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class OrderController : Controller
{
    private readonly OrderService orderService;
    private readonly AppDbContext appDbContext;
    public OrderController(OrderService orderService, AppDbContext appDbContext)
    {
        this.orderService = orderService;
        this.appDbContext = appDbContext;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto requestDto)
    {
        var result = await this.orderService.CreateOrder(requestDto);

        return Ok(new SuccessResponseDto()
        {
            message = "Sipariş oluşturuldu",
            data = result
        });
    }

    [HttpPost("[Action]")]
    [Authorize]
    public async Task<IActionResult> GetAllOrder([FromBody] GetAllOrderRequestDto getAllRequestDto)
    {
        var userId = User.Claims.FirstOrDefault(item => item.Type == "id")?.Value;
        var result = await this.orderService.GetAllOrderService(int.Parse(userId), getAllRequestDto);
        return Ok(new SuccessResponseDto()
        {
            message = "Başarılı",
            data = result
        });

    }


    [HttpGet("[Action]/{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        var result = await this.orderService.GetByOrder(id);

        return Ok(new SuccessResponseDto()
        {
            message = "Başarılı",
            data = result
        });
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetOrderStatusList()
    {
        var resut = await appDbContext.orderStatus.ToListAsync();

        return Ok(new SuccessResponseDto()
        {
            message = "Başarılı",
            data = resut
        });
    }

    [HttpPut("[Action]")]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDto updateOrderDto)
    {

        var result = await this.orderService.UpdateOrder(updateOrderDto);

        return Ok(new SuccessResponseDto()
        {
            message = "",
            data = result
        });
    }

    [HttpPut("[Action]")]
    /* [Authorize] */
    public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderUpdateStatusDto request)
    {
        var result = await this.orderService.OrderUpdateStatus(request);


        return Ok(new SuccessResponseDto(){
            message = "Güncelleme başarılı",
            data = result
        });
    }
}