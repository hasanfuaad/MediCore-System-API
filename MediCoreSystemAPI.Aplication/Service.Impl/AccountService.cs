using Application.Common;
using Application.Service.Impl.BaseServiceImpl;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.Service.Impl
{
    public class AccountService : BaseService<Account, AccountDTO>, IAccountService
    {
        public AccountService(IAccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
        : base(accountRepository, mapper, logger)
        {
        }
        public override async Task<Results<int>> InsertAsync(AccountDTO value)
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
        
        public async Task<Results<IEnumerable<AccountDTO>>> GetAllAsync()
        {
            return SuccessResult.Success(_mapper.Map<IEnumerable<AccountDTO>>(await _repository.GetAllAsync()));
        }

        public async Task<Results<AccountDTO>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الحساب غير موجود.", 404);
                    return ErrorResult.Failed<AccountDTO>(error);
                }

                var dto = _mapper.Map<AccountDTO>(entity);

                return SuccessResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {Id}", id);

                return ErrorResult.Failed<AccountDTO>(
                    Errors : "500"
                );
            }
        }


        public override async Task<Results<int>> UpdateAsync(int id, AccountDTO value)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                    return ErrorResult.Failed<int>("الحساب غير موجود.");

                // تحديث الحقول فقط
                entity.ProfilePic = value.ProfilePic;
                entity.FirstName = value.FirstName ?? entity.FirstName;
                entity.SecondName = value.SecondName ?? entity.SecondName;
                entity.ThirdName = value.ThirdName ?? entity.ThirdName;
                entity.LastName = value.LastName ?? entity.LastName;
                entity.Email = value.Email ?? entity.Email;
                entity.UserName = value.UserName ?? entity.UserName;
                entity.Desc = value.Desc ?? entity.Desc;


                await _repository.UpdateAsync(entity);

                return SuccessResult.Success(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update Account");
                return ErrorResult.Failed<int>(ex.Message);
            }
        }



        public async Task<Results<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    var error = ServiceError.CustomMessage("الحساب غير موجود.", 404);
                    return ErrorResult.Failed<bool>(error);
                }

                await _repository.DeleteAsync(id);

                return SuccessResult.Success(true); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {Id}", id);

                return ErrorResult.Failed<bool>(
                    Errors: "500"
                );
            }
        }


    }
}

