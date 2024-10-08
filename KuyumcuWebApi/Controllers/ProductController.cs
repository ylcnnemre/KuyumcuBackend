using KuyumcuWebApi.dto;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuyumcuWebApi.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class ProductController : Controller
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService, FileService fileService)
    {
        this._productService = productService;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> GetAllProducts([FromBody] PagedRequestDto pagedRequestDto  )
    {
        var result = await _productService.getAllProductAsync(pagedRequestDto);
        return Ok(new SuccessResponseDto()
        {
            message = "işlem başarılı",
            data = result
        });
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto createProductRequestDto)
    {
        var data = await _productService.createProduct(createProductRequestDto);
        return Ok(new SuccessResponseDto()
        {
            message = "ürün eklendi",
            data = data
        });
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> getByProductId(int productId)
    {

        var result = await _productService.getByProduct(productId);
        return Ok(new SuccessResponseDto()
        {
            message = "İşlem başarılı",
            data = result
        });
    }


    [HttpPut("[Action]")]
    public async Task<IActionResult> updateProduct([FromBody] UpdateProductRequestDto updateProductRequestDto)
    {
        var result = await _productService.updateProduct(updateProductRequestDto);
        return Ok(new SuccessResponseDto()
        {
            message = "ürün güncellendi",
            data = result
        });
    }

    [HttpDelete("[Action]/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> deleteProduct(int id)
    {
       await this._productService.deleteProduct(id);

       return Ok(new SuccessResponseDto(){
         message = "İşlem başarılı",
         data = null
       });
    }
}