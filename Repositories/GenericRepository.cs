using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AutoKitApi.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;


    public GenericRepository(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = DbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> Query() => DbSet.AsQueryable();

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }
    
    public virtual async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? configureQuery = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(predicate);
        if (configureQuery != null) query = configureQuery(query);
        return await query.ToListAsync(cancellationToken);
    }
    
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var entry = await DbSet.AddAsync(entity);
        return entry.Entity;
    }
    
    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }
    
    public virtual void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Remove(entity);
    }
}