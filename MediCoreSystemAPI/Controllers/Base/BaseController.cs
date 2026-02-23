using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReservePro.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> Logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }

        protected IActionResult HandleResponse<T>(T result, string message = null)
        {
            if (result == null)
            {
                Logger.LogWarning("Requested resource not found.");
                return NotFound(new { Message = "Resource not found." });
            }

            return Ok(new
            {
                Message = message ?? "Request successful.",
                Data = result
            });
        }

        protected IActionResult HandleError(Exception ex, string message = "An unexpected error occurred.")
        {
            Logger.LogError(ex, message);
            return StatusCode(500, new
            {
                Message = message,
                Error = ex.Message
            });
        }
    }
}
