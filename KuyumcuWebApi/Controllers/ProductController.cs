using KuyumcuWebApi.dto;
using KuyumcuWebApi.Service;
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

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _productService.getAllProductAsync();
        return Ok(result);
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto createProductRequestDto)
    {
        var data = await _productService.createProduct(createProductRequestDto);
        return Ok(data);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> getByProductId(int productId)
    {

        var result = await _productService.getByProduct(productId);
        return Ok(result);
    }


    [HttpPut("[Action]")]
    public async Task<IActionResult> updateProduct([FromBody] UpdateProductRequestDto updateProductRequestDto )
    {
        var result= await _productService.updateProduct(updateProductRequestDto);
        return Ok(result);
    }

}   