using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoreSystemAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ILogger<UsersController> logger, IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly ILogger<UsersController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<UserDTO>>>> Get()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get All Users");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Results<UserDTO>>> Get(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (!result.Success)
                {
                    var statusCode = result.Code != 0 ? result.Code : 400;
                    return StatusCode(statusCode, result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Account By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // استدعاء الخدمة مباشرة، وسيتم تعيين AccountId وRoleId داخل الخدمة
                var result = await _service.InsertAsync(model);

                // تحقق من النتيجة باستخدام SuccessResult/ErrorResult
                if (result is null)
                    return BadRequest(ErrorResult.Failed("حدث خطأ أثناء إنشاء المستخدم"));

                return Ok(SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create User");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }




        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);

                if (!result.Success)
                {
                    var statusCode = result.Code != 0 ? result.Code : 400;
                    return StatusCode(statusCode, result);
                }
                var accountResult = await _service.GetByIdAsync(id);

                if (!accountResult.Success)
                {
                    return StatusCode(accountResult.Code != 0 ? accountResult.Code : 500, accountResult);
                }

                return Ok(accountResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update Account");
                var error = ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}"
                );

                return StatusCode(500, ErrorResult.Failed(error));
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result.Success)
                {
                    var statusCode = result.Code != 0 ? result.Code : 400;
                    return StatusCode(statusCode, result);
                }

                return StatusCode(200, SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Account By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }
    }
}
