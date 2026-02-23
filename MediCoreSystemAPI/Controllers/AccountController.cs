using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymCoreSystemAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(ILogger<AccountController> logger, IAccountService service) : ControllerBase
    {
        private readonly IAccountService _service = service;
        private readonly ILogger<AccountController> _logger = logger;

        [Authorize]
        [HttpGet]

        public async Task<ActionResult<Results<IEnumerable<AccountDTO>>>> Get()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get All Accounts");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Results<AccountDTO>>> Get(int id)
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
        public async Task<IActionResult> Post([FromBody] AccountDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.InsertAsync(model);
                    return StatusCode(200, SuccessResult.Success(ServiceSuccess.Default));
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create Account");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountDTO model)
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
