using AutoKitApi.DTOs;
using AutoKitApi.Models;
using AutoKitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoKitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
    {
        await _productService.Add(productDto);

        var response = ApiResponseDto.SuccessResponse("Product created successfully.");

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAll();
        
        var response = ApiResponseDto.SuccessResponse<List<Product>>(products, "Product list successfully.");
        return Ok(response);
    }
    
    
}