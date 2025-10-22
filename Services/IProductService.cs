using AutoKitApi.DTOs;
using AutoKitApi.Models;

namespace AutoKitApi.Services;

public interface IProductService
{
    Task Add(ProductCreateDto  productDto);

    Task<List<Product>> GetAll();
}