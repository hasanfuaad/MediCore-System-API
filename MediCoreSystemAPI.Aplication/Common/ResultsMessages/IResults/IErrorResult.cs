
namespace Application.Common
{
    public interface IErrorResult
    {
        IReadOnlyCollection<ServiceError> Errors { get; }
    }
}
