using Application.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IService.IBaseService
{
    public interface IBaseServices<TDto> where TDto : class
    {
        Task<Results<IEnumerable<TDto>>> GetAllAsync();
        Task<Results<TDto>> GetByIdAsync(int id);
        Task<Results<int>> InsertAsync(TDto value);
        Task<Results<int>> UpdateAsync(int id, TDto value);
        Task<Results<int>> DeleteAsync(int id);
    }
}