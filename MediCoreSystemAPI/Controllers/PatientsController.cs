using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientssController(ILogger<PatientssController> logger, IPatientsService service) : ControllerBase
    {
        private readonly IPatientsService _service = service;
        private readonly ILogger<PatientssController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<PatientsDTO>>>> Get()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get All Patientss");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}")));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Results<PatientsDTO>>> Get(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (!result.Success)
                    return StatusCode(result.Code != 0 ? result.Code : 400, result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Patients By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage($"ExpMsg: {ex.Message}")));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);

                if (result is null)
                    return BadRequest(ErrorResult.Failed("خطأ أثناء إضافة المريض"));

                return Ok(SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create Patients");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage($"ExpMsg: {ex.Message}")));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PatientsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);

                if (!result.Success)
                    return StatusCode(result.Code != 0 ? result.Code : 400, result);

                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update Patients");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage($"ExpMsg: {ex.Message}")));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result.Success)
                    return StatusCode(result.Code != 0 ? result.Code : 400, result);

                return Ok(SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete Patients");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage($"ExpMsg: {ex.Message}")));
            }
        }
    }
}