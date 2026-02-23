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
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.Service.Impl
{
    public class PermissionService : BaseService<Permissions, PermissionsDTO>, IPermissionsService
    {

        public PermissionService(IPermissionRepository permissionsRepository, IMapper mapper, ILogger<PermissionService> logger)
            : base(permissionsRepository, mapper, logger)
        {
        }


        public  async Task<Results<int>> InsertAsync(PermissionsDTO value)
        {
            try
            {

                return await base.InsertAsync(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting user");
                return ErrorResult.Failed<int>(ex.Message);
            }
            
        }
        public async Task<Results<IEnumerable<PermissionsDTO>>> GetAllAsync()
        {
            var permissions = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<PermissionsDTO>>(permissions);
            return SuccessResult.Success(result);
        }

        public async Task<Results<PermissionsDTO>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الصلاحية غير موجود.", 404);
                    return ErrorResult.Failed<PermissionsDTO>(error);
                }

                var dto = _mapper.Map<PermissionsDTO>(entity);

                return SuccessResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء جلب بيانات الصلاحيه بالمعرف {Id}", id);
                return ErrorResult.Failed<PermissionsDTO>("حدث خطأ داخلي أثناء جلب بيانات الصلاحيه.");
            }
        }

        public async Task<Results<int>> UpdateAsync(int id, PermissionsDTO value)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return ErrorResult.Failed<int>("الصلاحيه غير موجود.");

                var existingName = (await _repository.GetAllAsync())
                    .FirstOrDefault(a => a.Name_Ar == value.Name_Ar && a.Id != id);

                if (existingName != null)
                    return ErrorResult.Failed<int>("اسم الصلاحيه موجود مسبقاً.");

                entity.Name_Ar = value.Name_Ar ?? entity.Name_Ar;
                entity.Name_En = value.Name_En ?? entity.Name_En;




                await _repository.UpdateAsync(entity);

                return SuccessResult.Success(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء تعديل الصلاحيه بالمعرف {Id}", id);
                return ErrorResult.Failed<int>("حدث خطأ أثناء تعديل الصلاحيه.");
            }
        }

        public async Task<Results<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الصلاحيه غير موجود.", 404);
                    return ErrorResult.Failed<bool>(error);
                }

                await _repository.DeleteAsync(id);

                return SuccessResult.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء حذف الصلاحيه بالمعرف {Id}", id);
                return ErrorResult.Failed<bool>("حدث خطأ داخلي أثناء حذف الصلاحيه.");
            }
        }
    }
}
