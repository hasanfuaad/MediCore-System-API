using Application.Common;


namespace Application.IService
{
    public interface IBaseServices<TDto> where TDto : class
    {
        Task<Results<IEnumerable<TDto>>> GetAllAsync();
        Task<Results<TDto>> GetByIdAsync(int id);
        Task<Results<int>> InsertAsync(TDto value);
        Task<Results<int>> UpdateAsync(int id, TDto value);
        Task<Results<bool>> DeleteAsync(int id);
    }
}
