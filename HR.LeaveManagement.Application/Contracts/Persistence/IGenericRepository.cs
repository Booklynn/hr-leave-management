using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task CreateAsync(TEntity entity);
    Task<IReadOnlyList<TEntity>> GetListAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
