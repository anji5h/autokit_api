using AutoKitApi.Enums;
using AutoKitApi.Models;
using AutoKitApi.Repositories;

namespace AutoKitApi.Services;

public class BagService : IBagService
{
    private readonly IGenericRepository<Bag> _bagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BagService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _bagRepository = unitOfWork.Repository<Bag>();
    }

    public async Task Add(String shippingAddress)
    {
        var newBag = new Bag()
        {
            ShippingAddress = shippingAddress
        };

        await _bagRepository.AddAsync(newBag);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Bag>> GetAll()
    {
        var bags = await _bagRepository.GetAllAsync();
        return bags.ToList();
    }
    
    public async Task UpdateStatus(int bagId, BagStatus status)
    {
        var bag = await _bagRepository.GetAsync(x => x.BagId == bagId);

        if (bag != null)
        {
            bag.Status = status;
            bag.UpdatedAt = DateTime.UtcNow;

            _bagRepository.Update(bag);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}