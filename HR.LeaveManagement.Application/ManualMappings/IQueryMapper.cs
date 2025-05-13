using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.ManualMappings;

public interface IQueryMapper<TEntity, TDto> where TEntity : BaseEntity where TDto : class
{
    TDto Map(TEntity source);

    IEnumerable<TDto> MapMany(IEnumerable<TEntity> sources)
    {
        return sources?.Select(Map) ?? [];
    }
}
