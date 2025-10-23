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

    [HttpPost]
    public async Task<IActionResult> CreateOperation([FromQuery] int bagId, [FromQuery] int productId,
        [FromQuery] int quantity)
    {
        await _operationService.Add(productId, bagId, quantity);

        var response =
            ApiResponseDto.SuccessResponse(
                $"Operation created. ProductId: {productId}, BagId: {bagId}, Quantity: {quantity}");
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetOperations()
    {
        var operations = await _operationService.GetAll();

        var response = ApiResponseDto.SuccessResponse(operations, "operations fetched successfully");
        return Ok(response);
    }
}