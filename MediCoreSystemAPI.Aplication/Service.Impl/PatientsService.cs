using Application.Common;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;

public class PatientsService : BaseService<Patients, PatientsDTO>, IPatientsService
{
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

    public PatientsService(
        IPatientsRepository repository,
        IMapper mapper,
        ILogger<PatientsService> logger,
        Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        : base(repository, mapper, logger)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<Results<int>> InsertAsync(PatientsDTO value)
    {
        try
        {
            if (value == null)
                return ErrorResult.Failed<int>("البيانات غير صالحة");

            var entity = _mapper.Map<Patients>(value);

            entity.CreatedAt = DateTime.Now;
            entity.Status = true;

            await _repository.InsertAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting Patients");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }

    public override async Task<Results<int>> UpdateAsync(int id, PatientsDTO value)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<int>("المريض غير موجود.");

            _mapper.Map(value, entity);

            entity.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "حدث خطأ أثناء تعديل المريض بالمعرف {Id}", id);
            return ErrorResult.Failed<int>("حدث خطأ أثناء تعديل بيانات المريض.");
        }
    }

    public async Task<Results<PatientsDTO>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<PatientsDTO>("المريض غير موجود.");

            return SuccessResult.Success(_mapper.Map<PatientsDTO>(entity));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ أثناء جلب المريض {Id}", id);
            return ErrorResult.Failed<PatientsDTO>("خطأ أثناء جلب بيانات المريض.");
        }
    }

    public async Task<Results<IEnumerable<PatientsDTO>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return SuccessResult.Success(_mapper.Map<IEnumerable<PatientsDTO>>(entities));
    }

    public async Task<Results<bool>> DeleteAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<bool>("المريض غير موجود.");

            await _repository.DeleteAsync(id);

            return SuccessResult.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ أثناء حذف المريض {Id}", id);
            return ErrorResult.Failed<bool>("خطأ أثناء حذف المريض.");
        }
    }
}