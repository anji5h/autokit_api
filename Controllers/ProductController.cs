using AutoKitApi.DTOs;
using AutoKitApi.Services;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Color = System.Drawing.Color;

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
        var payload =
            $"{baseUrl}/api/operation?bagId={qrCreateDto.BagId}&productId={qrCreateDto.ProductId}&quantity={qrCreateDto.Quantity}";
        
        using var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);

        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(
            pixelsPerModule: 10,
            darkColor: Color.Black,
            lightColor: Color.White,
            drawQuietZones: true);

        var qrCodeName = $"qr_code_{DateTime.Now.Ticks}.png";
        return File(qrCodeBytes, "image/png", qrCodeName);
    }
}