using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class OrderController:Controller {

    [HttpPost("[Action]")]
    public async Task<IActionResult> createOrder(){

        
        return Ok("ads");
    }
}