using AutoKitApi.DTOs;
using AutoKitApi.Models;

namespace AutoKitApi.Services;

public interface IProductService
{
    Task AddProduct(ProductCreateDto  productDto);

    Task<List<Product>> GetAllProducts();
}