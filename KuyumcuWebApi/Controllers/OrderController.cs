using KuyumcuWebApi.dto;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class OrderController : Controller
{
    private readonly OrderService orderService;
    
    public OrderController(OrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto requestDto)
    {
        var result= await this.orderService.CreateOrder(requestDto);

        return Ok(new SuccessResponseDto(){
            message = "Sipariş oluşturuldu",
            data = result 
        });
    }
}