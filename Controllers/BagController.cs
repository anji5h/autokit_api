using AutoKitApi.DTOs;
using AutoKitApi.Enums;
using AutoKitApi.Models;
using AutoKitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoKitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BagController:ControllerBase
{
    private readonly IBagService _bagService;

    public BagController(IBagService bagService)
    {
        _bagService = bagService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBag([FromQuery]string shippingAddress)
    {
        await _bagService.Add(shippingAddress);

        var response = ApiResponseDto.SuccessResponse("Bag created");
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bags = await _bagService.GetAll();
        
        var response = ApiResponseDto.SuccessResponse(bags, "Bag list successfully.");
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateStatus([FromQuery] int bagId, [FromQuery] BagStatus status)
    {
        await _bagService.UpdateStatus(bagId, status);

        var response = ApiResponseDto.SuccessResponse($"Bag status updated to {status}");
        return Ok(response);
    }
}