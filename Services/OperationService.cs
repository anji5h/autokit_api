using AutoKitApi.Models;
using AutoKitApi.Repositories;

namespace AutoKitApi.Services;
public class OperationService : IOperationService
{
    private readonly IGenericRepository<Operation>  _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OperationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _operationRepository = unitOfWork.Repository<Operation>();
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
        await _unitOfWork.SaveChangesAsync();
    }
}