using AutoKitApi.DTOs;
using AutoKitApi.Models;

namespace AutoKitApi.Services;

public interface IProductService
{
    Task Add(ProductCreateDto  productDto);
    
    Task<bool> CheckProductQuantity(int productId, int quantity);

    Task<List<Product>> GetAll();
}