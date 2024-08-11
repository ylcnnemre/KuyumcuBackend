using KuyumcuWebApi.dto;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class ProductPhotoController : Controller
{
    private readonly ProductImageService productPhotoService;
    public ProductPhotoController(ProductImageService productPhotoService)
    {
        this.productPhotoService = productPhotoService;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> AddProductPhoto([FromForm] AddProductPhotoDto addProductPhotoDto)
    {
        var response = await productPhotoService.AddProductPhoto(addProductPhotoDto);
        return Ok(new SuccessResponseDto(){
            message = "Fotoğraf eklendi",
            data = response
        });
    }

    [HttpDelete("{photoId}")]
    public async Task<IActionResult> deletePhoto(int photoId){
         await productPhotoService.deletePhoto(photoId);

         return Ok(new SuccessResponseDto(){
            message = "Fotoğraf başarıyla silindi",
            data = null
         });

    }
}