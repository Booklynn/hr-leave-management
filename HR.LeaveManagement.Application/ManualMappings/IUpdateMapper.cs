using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.ManualMappings;

public interface IUpdateMapper<TDto, TEntity> where TDto : class where TEntity : BaseEntity
{
    TEntity Map(TDto dto, TEntity existingEntity);
}
