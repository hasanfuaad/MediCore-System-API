using Application.Common;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;

using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;

public class DepartmentsService : BaseService<Departments, DepartmentsDTO>, IDepartmentsService
{
    public DepartmentsService(
        IDepartmentsRepository repository,
        IMapper mapper,
        ILogger<DepartmentsService> logger)
        : base(repository, mapper, logger)
    {
    }

    public override async Task<Results<int>> InsertAsync(DepartmentsDTO value)
    {
        try
        {
            var entity = _mapper.Map<Departments>(value);

            entity.CreatedAt = DateTime.Now;
            entity.Status = true;

            await _repository.InsertAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting Departments");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }

    public override async Task<Results<int>> UpdateAsync(int id, DepartmentsDTO value)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<int>("القسم غير موجود.");

            _mapper.Map(value, entity);

            entity.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Departments {Id}", id);
            return ErrorResult.Failed<int>("خطأ أثناء تعديل القسم.");
        }
    }

    public async Task<Results<DepartmentsDTO>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<DepartmentsDTO>("القسم غير موجود.");

        return SuccessResult.Success(_mapper.Map<DepartmentsDTO>(entity));
    }

    public async Task<Results<IEnumerable<DepartmentsDTO>>> GetAllAsync()
    {
        return SuccessResult.Success(_mapper.Map<IEnumerable<DepartmentsDTO>>(await _repository.GetAllAsync()));
    }

    public async Task<Results<bool>> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<bool>("القسم غير موجود.");

        await _repository.DeleteAsync(id);

        return SuccessResult.Success(true);
    }
}