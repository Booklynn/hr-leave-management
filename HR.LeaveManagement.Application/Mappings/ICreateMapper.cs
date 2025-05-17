using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Mappings;

public interface ICreateMapper<TSource, TEntity> where TSource : class where TEntity : BaseEntity
{
    TEntity Map(TSource source);
}
