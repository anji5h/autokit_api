using AutoKitApi.Enums;
using AutoKitApi.Models;

namespace AutoKitApi.Services;

public interface IBagService
{
    Task Add(String shippingAddress);

    Task<List<Bag>> GetAll();

    Task<bool> CheckBag(int bagId);
    
    Task UpdateStatus(int bagId, BagStatus status);
}