using System.Drawing;
using System.Drawing.Imaging;
using AutoKitApi.DTOs;
using AutoKitApi.Models;
using AutoKitApi.Services;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace AutoKitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IBagService _bagService;
    private readonly IConfiguration _configuration;

    public ProductController(IProductService productService, IBagService bagService, IConfiguration configuration)
    {
        _productService = productService;
        _bagService = bagService;
        _configuration = configuration;
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

        var response = ApiResponseDto.SuccessResponse(products, "Product list successfully.");
        return Ok(response);
    }

    [HttpPost]
    [Route("/api/[controller]/qrcode")]
    public IActionResult GenerateQrCode([FromBody] QrCreateDto qrCreateDto)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var url = $"{baseUrl}/api/operation?bagId={qrCreateDto.BagId}&productId={qrCreateDto.ProductId}&quantity={qrCreateDto.Quantity}";

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new QRCode(qrCodeData);
        using var qrBitmap = qrCode.GetGraphic(5);

        using var stream = new MemoryStream();
        qrBitmap.Save(stream, ImageFormat.Png);
        var imageBytes = stream.ToArray();

        var qrCodeName = $"qr_code_{DateTime.Now.Ticks}.png";
        return File(imageBytes, "image/png", qrCodeName);
    }
}