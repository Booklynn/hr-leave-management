namespace HR.LeaveManagement.Application.Mappings;

public interface ICreateMapper<TSource, TDTO> where TSource : class where TDTO : class
{
    TDTO Map(TSource source);
}
