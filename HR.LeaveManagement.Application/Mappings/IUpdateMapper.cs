using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Mappings;

public interface IUpdateMapper<TDTO, TEntity> where TDTO : class where TEntity : BaseEntity
{
    void Map(TDTO dto, TEntity entity);
}
