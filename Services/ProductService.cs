using AutoKitApi.DTOs;
using AutoKitApi.Models;
using AutoKitApi.Repositories;

namespace AutoKitApi.Services;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product>  _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = unitOfWork.Repository<Product>();
    }

    public async Task Add(ProductCreateDto  productDto)
    {
        var productName = productDto.Name.Trim().ToLower();
        
        var product = await _productRepository.GetAsync(x => x.Name == productName);

        if (product == null)
        {
            var newProduct = new Product()
            {
                Name = productName,
                Description = productDto.Description?.Trim().ToLower(),
                Quantity = productDto.Quantity,
                Location = productDto.Location
            };
            await _productRepository.AddAsync(newProduct);
        }
        else
        {
            product.Quantity += productDto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            _productRepository.Update(product);
        }
        
        await _unitOfWork.SaveChangesAsync();
    }
    
    
    public async Task<List<Product>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        
        return products.ToList();
    }
}