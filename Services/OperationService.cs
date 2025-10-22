using AutoKitApi.Models;
using AutoKitApi.Repositories;

namespace AutoKitApi.Services;
public class OperationService : IOperationService
{
    private readonly IGenericRepository<Operation>  _operationRepository;
    private readonly IGenericRepository<Product>  _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OperationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _operationRepository = unitOfWork.Repository<Operation>();
        _productRepository = unitOfWork.Repository<Product>();
    }

    public async Task Add(int productId, int bagId, int quantity)
    {
        var newOperation = new Operation()
        {
            ProductId = productId,
            BagId = bagId,
            Quantity = quantity
        };

        await _operationRepository.AddAsync(newOperation);
        
        var product = await _operationRepository.GetAsync(x => x.ProductId == productId);
        
        if (product != null)
        {
            product.Quantity -= quantity;

            _operationRepository.Update(product);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}