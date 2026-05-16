using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using System.Linq.Expressions;

namespace HealthcareHospitalManagement.Domain.Common.IGenericRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    // Existing methods...

    Task<T?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct);
    Task AddAsync(T entity, CancellationToken ct);
    Task UpdateAsync(T entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task SoftDeleteAsync(Guid id, CancellationToken ct);
    Task RestoreAsync(Guid id, CancellationToken ct);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken ct);

    Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken ct);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate, CancellationToken ct);

    IQueryable<T> GetQueryable();
    Task<PagedResponse<T>> ToPagedAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken ct);

    IAsyncEnumerable<T> GetAllAsAsyncEnumerable();
    IAsyncEnumerable<T> FindAsAsyncEnumerable(Expression<Func<T, bool>> predicate);

    // 🔹 নতুন method
    void Remove(T entity);
}


