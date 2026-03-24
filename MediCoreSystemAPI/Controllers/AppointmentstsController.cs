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
    public class AppointmentssController(ILogger<AppointmentssController> logger, IAppointmentsService service) : ControllerBase
    {
        private readonly IAppointmentsService _service = service;
        private readonly ILogger<AppointmentssController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<AppointmentsDTO>>>> Get()
            => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Results<AppointmentsDTO>>> Get(int id)
            => await _service.GetByIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(AppointmentsDTO model)
        {
            var result = await _service.InsertAsync(model);
            return Ok(SuccessResult.Success(ServiceSuccess.Default));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AppointmentsDTO model)
        {
            var result = await _service.UpdateAsync(id, model);
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(SuccessResult.Success(ServiceSuccess.Default));
        }
    }
}