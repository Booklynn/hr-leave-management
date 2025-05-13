namespace HR.LeaveManagement.Application.Mappings;

public interface ICreateMapper<TSource, TDto> where TSource : class where TDto : class
{
    TDto Map(TSource source);
}
