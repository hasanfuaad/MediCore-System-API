using Application.Common;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;

using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;

public class AppointmentsService : BaseService<Appointments, AppointmentsDTO>, IAppointmentsService
{
    public AppointmentsService(
        IAppointmentsRepository repository,
        IMapper mapper,
        ILogger<AppointmentsService> logger)
        : base(repository, mapper, logger)
    {
    }

    public override async Task<Results<int>> InsertAsync(AppointmentsDTO value)
    {
        try
        {
            var entity = _mapper.Map<Appointments>(value);

            entity.CreatedAt = DateTime.Now;
            entity.Status = true;

            await _repository.InsertAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting Appointments");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }

    public override async Task<Results<int>> UpdateAsync(int id, AppointmentsDTO value)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ErrorResult.Failed<int>("الموعد غير موجود.");

            _mapper.Map(value, entity);

            entity.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(entity);

            return SuccessResult.Success(entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Appointments {Id}", id);
            return ErrorResult.Failed<int>("خطأ أثناء تعديل الموعد.");
        }
    }

    public async Task<Results<AppointmentsDTO>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<AppointmentsDTO>("الموعد غير موجود.");

        return SuccessResult.Success(_mapper.Map<AppointmentsDTO>(entity));
    }

    public async Task<Results<IEnumerable<AppointmentsDTO>>> GetAllAsync()
    {
        return SuccessResult.Success(_mapper.Map<IEnumerable<AppointmentsDTO>>(await _repository.GetAllAsync()));
    }

    public async Task<Results<bool>> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ErrorResult.Failed<bool>("الموعد غير موجود.");

        await _repository.DeleteAsync(id);

        return SuccessResult.Success(true);
    }
}