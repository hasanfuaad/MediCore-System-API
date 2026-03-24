using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorssController(ILogger<DoctorssController> logger, IDoctorsService service) : ControllerBase
    {
        private readonly IDoctorsService _service = service;
        private readonly ILogger<DoctorssController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<DoctorsDTO>>>> Get()
        {
            try { return await _service.GetAllAsync(); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Doctorss");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(ex.Message)));
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Results<DoctorsDTO>>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.Success ? Ok(result) : StatusCode(result.Code != 0 ? result.Code : 400, result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorsDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.InsertAsync(model);
            return Ok(SuccessResult.Success(ServiceSuccess.Default));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorsDTO model)
        {
            var result = await _service.UpdateAsync(id, model);
            if (!result.Success)
                return StatusCode(result.Code != 0 ? result.Code : 400, result);

            return Ok(await _service.GetByIdAsync(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result.Success
                ? Ok(SuccessResult.Success(ServiceSuccess.Default))
                : StatusCode(result.Code != 0 ? result.Code : 400, result);
        }

    }
}