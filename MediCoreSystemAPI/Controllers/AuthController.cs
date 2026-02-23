using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Aplication.Service.Impl;
using MediCoreSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservePro.Management.Api.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GymCoreSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly AppDbContext _context;


        public AuthController(IAuthService authService, ILogger<BaseController> logger, IJwtService jwtService, AppDbContext context) 
            : base(logger)
        {
            _authService = authService;
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            // 1) البحث عن Account
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (account != null)
            {
                if (account.Password != dto.Password)
                {
                    return Unauthorized(ErrorResult<object>.Failed(
                        ServiceError.CustomMessage("فشلت عملية المصادقة، الايميل او كلمة المرور غير صحيحه", 401)
                    ));
                }

                var token = _jwtService.GenerateToken(account.Id, null, roleId: 1, isAccountOwner: true);

                return Ok(new SuccessResult<AuthResponseDTO>(new AuthResponseDTO
                {
                    Token = token,
                    AccountId = account.Id,
                    UserId = null,
                    RoleId = 1,
                    IsAccountOwner = true
                }));

            }

            // 2) البحث عن User
            var user = await _context.Users.Include(x => x.Account).FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return Unauthorized(new ErrorResult<object>(
     ServiceError.CustomMessage("فشلت عملية المصادقة، الايميل او كلمة المرور غير صحيحه", 401)
 ));

            }

            var userToken = _jwtService.GenerateToken(user.AccountId, user.Id, user.RoleId, false);

            return Ok(new SuccessResult<AuthResponseDTO>(new AuthResponseDTO
            {
                Token = userToken,
                AccountId = user.AccountId,
                UserId = user.Id,
                RoleId = user.RoleId,
                IsAccountOwner = false
            }));
        }


        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    return HandleError(new Exception("User ID claim not found."), "Invalid token.");

                int accountId = int.Parse(userIdClaim.Value);

                var result = await _authService.ChangePasswordAsync(accountId, dto);

                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to change password.");

                return HandleResponse(result, "Password changed successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while changing password.");
            }
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var result = await _authService.LogoutAsync(userId);
            return Ok(result);
        }
    }
}
