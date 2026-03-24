using Application.Common;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentssController(ILogger<DepartmentssController> logger, IDepartmentsService service) : ControllerBase
    {
        private readonly IDepartmentsService _service = service;
        private readonly ILogger<DepartmentssController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<DepartmentsDTO>>>> Get()
            => await _service.GetAllAsync();

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Results<DepartmentsDTO>>> Get(int id)
            => await _service.GetByIdAsync(id);

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(DepartmentsDTO model)
        {
            var result = await _service.InsertAsync(model);
            return Ok(SuccessResult.Success(ServiceSuccess.Default));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartmentsDTO model)
        {
            var result = await _service.UpdateAsync(id, model);
            return Ok(await _service.GetByIdAsync(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(SuccessResult.Success(ServiceSuccess.Default));
        }
    }
}