using AutoKitApi.Models;

namespace AutoKitApi.Services;

public interface IOperationService
{
    Task Add(int productId, int bagId, int quantity);

    Task<List<Operation>> GetAll();
}