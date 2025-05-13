using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Mappings;

public interface IQueryMapper<TEntity, TDTO> where TEntity : BaseEntity where TDTO : class
{
    TDTO Map(TEntity source);

    IEnumerable<TDTO> MapMany(IEnumerable<TEntity> sources)
    {
        return sources?.Select(Map) ?? [];
    }
}
