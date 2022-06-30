using Microsoft.AspNetCore.Mvc;
namespace CXBIM.UserService.API.Controllers
{
    [Route("Health")]
    [ApiController]
    public class Health : ControllerBase
    {
        // GET: /Health
        [HttpGet]
        public IActionResult HealthyCheck()
        {
            return Ok();
        }
    }
}

