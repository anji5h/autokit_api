using AutoKitApi.DTOs;
using AutoKitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoKitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly IOperationService _operationService;

    public OperationController(IOperationService operationService)
    {
        _operationService = operationService;
    }

    [HttpGet]
    public async Task<IActionResult> CreateOperation([FromQuery] int bagId, [FromQuery] int productId,
        [FromQuery] int quantity)
    {
        await _operationService.Add(productId, bagId, quantity);
        
        var response = ApiResponseDto.SuccessResponse("Operation created");
        return Ok(response);
    }
}