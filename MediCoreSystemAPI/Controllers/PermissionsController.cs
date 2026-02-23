using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoreSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsService _service;
        private readonly ILogger<PermissionsController> _logger;

        public PermissionsController(ILogger<PermissionsController> logger, IPermissionsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<PermissionsDTO>>>> GetAll()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get All Permissions");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Results<PermissionsDTO>>> GetById(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Permission By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionsDTO model)
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
                _logger.LogError(ex, "Error in Create Permission");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PermissionsDTO model)
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
