using Application.Common;
using Application.IService;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.Service.Impl
{
    public class RolesService : BaseService<Roles, RolesDTO>, IRolesService
    {
        public RolesService(IRolesRepository rolesRepository, IMapper mapper, ILogger<RolesService> logger)
           : base(rolesRepository, mapper, logger)
        {
        }
         public async Task<Results<int>> InsertAsync(RolesDTO value)
        {
            try
            {

                return await base.InsertAsync(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting Roles");
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        public  async Task<Results<int>> UpdateAsync(int id, RolesDTO value)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return ErrorResult.Failed<int>("الدور غير موجود.");

                var existingName = (await _repository.GetAllAsync())
                    .FirstOrDefault(a => a.Name_Ar == value.Name_Ar && a.Id != id);

                if (existingName != null)
                    return ErrorResult.Failed<int>("اسم الدور موجود مسبقاً.");

                entity.Name_Ar = value.Name_Ar ?? entity.Name_Ar;
                entity.Name_En = value.Name_En ?? entity.Name_En;

                


                await _repository.UpdateAsync(entity);

                return SuccessResult.Success(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء تعديل الدور بالمعرف {Id}", id);
                return ErrorResult.Failed<int>("حدث خطأ أثناء تعديل الدور.");
            }
        }

        public async Task<Results<IEnumerable<RolesDTO>>> GetAllAsync()
        {
            return SuccessResult.Success(_mapper.Map<IEnumerable<RolesDTO>>(await _repository.GetAllAsync()));
        }

        public async Task<Results<RolesDTO>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الدور غير موجود.", 404);
                    return ErrorResult.Failed<RolesDTO>(error);
                }

                var dto = _mapper.Map<RolesDTO>(entity);

                return SuccessResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء جلب بيانات الدور بالمعرف {Id}", id);
                return ErrorResult.Failed<RolesDTO>("حدث خطأ داخلي أثناء جلب بيانات الدور.");
            }
        }

        public async Task<Results<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الدور غير موجود.", 404);
                    return ErrorResult.Failed<bool>(error);
                }

                await _repository.DeleteAsync(id);

                return SuccessResult.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء حذف الدور بالمعرف {Id}", id);
                return ErrorResult.Failed<bool>("حدث خطأ داخلي أثناء حذف الدور.");
            }
        }
    }
}
