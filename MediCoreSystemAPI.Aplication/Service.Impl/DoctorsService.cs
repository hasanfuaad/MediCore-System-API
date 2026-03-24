using Application.Common;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;
public class DoctorsService : BaseService<Doctors, DoctorsDTO>, IDoctorsService
{
    public DoctorsService(
        IDoctorsRepository repository,
        IMapper mapper,
        ILogger<DoctorsService> logger)
        : base(repository, mapper, logger)
    {
    }

    public override async Task<Results<int>> InsertAsync(DoctorsDTO value)
    {
        try
        {
            var entity = _mapper.Map<Doctors>(value);

            entity.CreatedAt = DateTime.Now;
            entity.Status = true;

            await _repository.InsertAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting Doctors");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }

    public override async Task<Results<int>> UpdateAsync(int id, DoctorsDTO value)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<int>("الطبيب غير موجود.");

            _mapper.Map(value, entity);

            entity.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Doctors {Id}", id);
            return ErrorResult.Failed<int>("حدث خطأ أثناء تعديل الطبيب.");
        }
    }

    public async Task<Results<DoctorsDTO>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<DoctorsDTO>("الطبيب غير موجود.");

        return SuccessResult.Success(_mapper.Map<DoctorsDTO>(entity));
    }

    public async Task<Results<IEnumerable<DoctorsDTO>>> GetAllAsync()
    {
        return SuccessResult.Success(_mapper.Map<IEnumerable<DoctorsDTO>>(await _repository.GetAllAsync()));
    }

    public async Task<Results<bool>> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<bool>("الطبيب غير موجود.");

        await _repository.DeleteAsync(id);

        return SuccessResult.Success(true);
    }
}