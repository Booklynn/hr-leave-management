using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Mappings;

public interface IUpdateMapper<Tsource, TEntity> where Tsource : class where TEntity : BaseEntity
{
    void Map(Tsource source, TEntity target);
}
