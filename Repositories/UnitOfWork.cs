using AutoKitApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoKitApi.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    private readonly Dictionary<Type, object> _repositories =  new();
    private bool _disposed;
    
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (_repositories.TryGetValue(type, out var repository))
        {
            return (IGenericRepository<TEntity>)repository!;
        }

        var newRepository = new GenericRepository<TEntity>(_dbContext);
        _repositories.Add(type, newRepository);
        return newRepository;
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _dbContext.Dispose();
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}