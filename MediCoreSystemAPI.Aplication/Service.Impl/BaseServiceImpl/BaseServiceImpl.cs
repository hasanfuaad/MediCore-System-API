using Application.Common;
using Application.IService.IBaseService;
using AutoMapper;
using MediCoreSystem.Aplication.Service.Impl;
using MediCoreSystem.Domain.IRepository;
using MediCoreSystem.Domain.IRepository.IBaseRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Impl.BaseServiceImpl
{
    public abstract class BaseService<TEntity, TDto> : IBaseServices<TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseService<TEntity, TDto>> _logger;
        private IRolesRepository rolesRepository;
        private ILogger<RolesService> logger;

        protected BaseService(
            IBaseRepository<TEntity> repository,
            IMapper mapper,
            ILogger<BaseService<TEntity, TDto>> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

    
        public virtual async Task<Results<IEnumerable<TDto>>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
                return SuccessResult.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all {Entity}", typeof(TEntity).Name);
                return ErrorResult.Failed<IEnumerable<TDto>>(ex.Message);
            }
        }

        public virtual async Task<Results<TDto>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("{Entity} with Id {Id} not found", typeof(TEntity).Name, id);
                    return ErrorResult.Failed<TDto>("Entity not found");
                }

                var dto = _mapper.Map<TDto>(entity);
                return SuccessResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching {Entity} with Id {Id}", typeof(TEntity).Name, id);
                return ErrorResult.Failed<TDto>(ex.Message);
            }
        }

        public virtual async Task<Results<int>> InsertAsync(TDto value)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(value);
                var result = await _repository.InsertAsync(entity);
                return SuccessResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting {Entity}", typeof(TEntity).Name);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        public virtual async Task<Results<int>> UpdateAsync(int id, TDto value)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(value);
                var prop = entity.GetType().GetProperty("Id");
                if (prop != null) prop.SetValue(entity, id);

                var result = await _repository.UpdateAsync(entity);
                return SuccessResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating {Entity} with Id {Id}", typeof(TEntity).Name, id);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        public virtual async Task<Results<int>> DeleteAsync(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                return SuccessResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting {Entity} with Id {Id}", typeof(TEntity).Name, id);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }
    }
}