using Application.Common;
using Application.IService;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


public class UserService : BaseService<Users, UserDTO>, IUserService
{
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

    public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
            : base(userRepository, mapper, logger)
    {
        _httpContextAccessor = httpContextAccessor;
    }



    public override async Task<Results<int>> InsertAsync(UserDTO value)
    {
        try
        {
            value.Password = BCrypt.Net.BCrypt.HashPassword(value.Password);

            var entity = _mapper.Map<Users>(value);

            var accountIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
    .FirstOrDefault(c => c.Type == "AccountId")?.Value;


            if (string.IsNullOrEmpty(accountIdClaim) || !int.TryParse(accountIdClaim, out int accountId))
                return ErrorResult.Failed<int>("AccountId غير موجود في التوكن.");

            entity.AccountId = accountId;
            entity.RoleId = value.RoleId;
            entity.IsActive = true;

            await _repository.InsertAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting user");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }




    public override async Task<Results<int>> UpdateAsync(int id, UserDTO value)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ErrorResult.Failed<int>("الحساب غير موجود.");

            var existingName = (await _repository.GetAllAsync())
                .FirstOrDefault(a => a.UserName == value.UserName && a.Id != id);
            if (existingName != null)
                return ErrorResult.Failed<int>("اسم المستخدم موجود مسبقاً لمستخدم آخر.");

            entity.FullName = value.FullName ?? entity.FullName;
            entity.UserName = value.UserName ?? entity.UserName;
            entity.PhoneNumber = value.PhoneNumber ?? entity.PhoneNumber;
            entity.IsActive = true;
            entity.RoleId = value.RoleId;
            entity.Email = value.Email ?? entity.Email;


            await _repository.UpdateAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "حدث خطأ أثناء تعديل المستخدم بالمعرف {Id}", id);
            return ErrorResult.Failed<int>("حدث خطأ أثناء تعديل بيانات المستخدم.");
        }
    }

    public async Task<Results<UserDTO>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                var error = ServiceError.CustomMessage("المستخدم غير موجود.", 404);
                return ErrorResult.Failed<UserDTO>(error);
            }

            var dto = _mapper.Map<UserDTO>(entity);

            return SuccessResult.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "حدث خطأ أثناء جلب بيانات المستخدم بالمعرف {Id}", id);
            return ErrorResult.Failed<UserDTO>("حدث خطأ داخلي أثناء جلب بيانات المستخدم.");
        }
    }
    public async Task<Results<IEnumerable<UserDTO>>> GetAllAsync()
    {
        return SuccessResult.Success(_mapper.Map<IEnumerable<UserDTO>>(await _repository.GetAllAsync()));
    }
    public async Task<Results<bool>> DeleteAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                var error = ServiceError.CustomMessage("المستخدم غير موجود.", 404);
                return ErrorResult.Failed<bool>(error);
            }

            await _repository.DeleteAsync(id);

            return SuccessResult.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "حدث خطأ أثناء حذف المستخدم بالمعرف {Id}", id);
            return ErrorResult.Failed<bool>("حدث خطأ داخلي أثناء حذف المستخدم.");
        }
    }
}