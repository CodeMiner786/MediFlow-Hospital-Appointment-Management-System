using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareHospitalManagement.Infrastructure.GenericRepository;

public class GenericRepository<T>(DbContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _dbSet.FindAsync([id], ct);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct)
        => await _dbSet.Where(e => !e.IsDeleted).ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct)
        => await _dbSet.AddAsync(entity, ct);

    // 🔹 Updated: শুধু DbSet এ update করবে, SaveChangesAsync UnitOfWork থেকে হবে
    public Task UpdateAsync(T entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync(ct);
        }
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity != null)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            await context.SaveChangesAsync(ct);
        }
    }

    public async Task RestoreAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity != null)
        {
            entity.IsDeleted = false;
            _dbSet.Update(entity);
            await context.SaveChangesAsync(ct);
        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        => await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync(ct);

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        => await _dbSet.Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate, ct);

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct)
        => await _dbSet.AddRangeAsync(entities, ct);

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken ct)
    {
        _dbSet.RemoveRange(entities);
        await context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken ct)
        => await _dbSet.Where(e => !e.IsDeleted).AsNoTracking().ToListAsync(ct);

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        => await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate, ct);

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate, CancellationToken ct)
        => predicate != null
            ? await _dbSet.Where(e => !e.IsDeleted).CountAsync(predicate, ct)
            : await _dbSet.Where(e => !e.IsDeleted).CountAsync(ct);

    public IQueryable<T> GetQueryable()
        => _dbSet.Where(e => !e.IsDeleted).AsQueryable();

    public async Task<PagedResponse<T>> ToPagedAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken ct)
    {
        var totalRecords = await query.CountAsync(ct);
        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return PagedResponse<T>.Create(data, pageNumber, pageSize, totalRecords);
    }

    public IAsyncEnumerable<T> GetAllAsAsyncEnumerable()
        => _dbSet.Where(e => !e.IsDeleted).AsAsyncEnumerable();

    public IAsyncEnumerable<T> FindAsAsyncEnumerable(Expression<Func<T, bool>> predicate)
        => _dbSet.Where(predicate).Where(e => !e.IsDeleted).AsAsyncEnumerable();

    // 🔹 নতুন method: সরাসরি entity remove করার জন্য
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}
