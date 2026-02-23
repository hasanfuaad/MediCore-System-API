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
    public class RolesController(ILogger<RolesController> logger, IRolesService service) : ControllerBase
    {
        private readonly IRolesService _service = service;
        private readonly ILogger<RolesController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<RolesDTO>>>> Get()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Results<RolesDTO>>> Get(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get User By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolesDTO model)
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
                _logger.LogError(ex, "Error in Create Role");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RolesDTO model)
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
