using Application.Common;
using AutoMapper;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;


        public AuthService(IUserRepository userRepo, IMapper mapper, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;

        }

        public async Task<Results<string>> LoginAsync(LoginDTO dto)
        {
            try
            {
                var user = await _userRepo.GetByEmailAsync(dto.Email);
                if (user == null)
                {
                    _logger.LogWarning("Failed login attempt for email {Email}", dto.Email);
                    return ErrorResult.Failed<string>("فشلت عملية المصادقة، الايميل او كلمة المرور غير صحيحه");
                }

                bool isPasswordValid = false;
                try
                {
                    isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
                }
                catch
                {
                    _logger.LogWarning("Password format invalid for email {Email}", dto.Email);
                }

                if (!isPasswordValid)
                {
                    _logger.LogWarning("Failed login attempt for email {Email}", dto.Email);
                    return ErrorResult.Failed<string>("فشلت عملية المصادقة، الايميل او كلمة المرور غير صحيحه");
                }

                // ➤ Generate Token using account + user data
                var token = _jwtService.GenerateToken(
                    user.AccountId,
                    user.Id,
                    user.RoleId, 
                    false            
                );

                _logger.LogInformation("User {Email} logged in successfully", dto.Email);

                return SuccessResult.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for {Email}", dto.Email);
                return ErrorResult.Failed<string>("An error occurred during login");
            }
        }

        public async Task<Results<bool>> ChangePasswordAsync(int id, ChangePasswordDTO dto)
        {
            try
            {
                var user = await _userRepo.GetByIdAsync(id); 
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
                {
                    _logger.LogWarning("Failed password change attempt for userId {UserId}", id);
                    return ErrorResult.Failed<bool>("Current password is incorrect");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _userRepo.UpdateAsync(user);

                _logger.LogInformation("Password changed successfully for userId {UserId}", id);
                return SuccessResult.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing password for userId {UserId}", id);
                return ErrorResult.Failed<bool>("An error occurred while changing password");
            }
        }



        public async Task<Results<bool>> LogoutAsync(int userId)
        {
            return SuccessResult.Success(true);
        }
    }

}
