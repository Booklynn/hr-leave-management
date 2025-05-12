using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.ManualMappings;

public interface IManualMapper<TSource, TDestination>
{
    TDestination ManualMap(TSource source);

    IEnumerable<TDestination> ManualMapMany(IEnumerable<TSource> sources)
    {
        return sources?.Select(ManualMap) ?? [];
    }
    public TDestination? MapUpdatedRequestToEntity(TSource source, BaseEntity target)
    {
        dynamic? result = ManualMap(source);
        if (result == null)
        {
            return default;
        }

        result.Id = target.Id;
        result.DateCreated = target.DateCreated;
        result.DateModified = target.DateModified;

        return result;
    }
}
