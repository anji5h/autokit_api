namespace AutoKitApi.Services;

public interface IOperationService
{
    Task Add(int productId, int bagId, int quantity);
}