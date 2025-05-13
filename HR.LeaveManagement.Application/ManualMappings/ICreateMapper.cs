namespace HR.LeaveManagement.Application.ManualMappings;

public interface ICreateMapper<TSource, TDto> where TSource : class where TDto : class
{
    TDto Map(TSource source);
}
