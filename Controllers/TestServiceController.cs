using DateInputNormalizer.Filters;
using DateInputNormalizer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateInputNormalizer.Controllers
{
    
    [ApiController]
    public class TestServiceController : ControllerBase
    {

        [HttpPost]
        [Route("api/[controller]/TestDateHandling")]
        [NormalizeDateInput]
        public IActionResult TestDateHandling([FromBody] TestDateModel model, [FromHeader(Name = "X-Timezone")] string timeZone)
        {
            return Ok(new
            {
                ServerReceived = model,
                ServerNow = DateTime.Now,
                ServerUtcNow = DateTime.UtcNow
            });
        }

        [HttpPost]
        [Route("api/[controller]/TestDateHandlingNoFilter")]
        [NormalizeDateInput]
        public IActionResult TestDateHandlingNoFilter([FromBody] TestDateModel model, [FromHeader(Name = "X-Timezone")] string timeZone)
        {
            return Ok(new
            {
                ServerReceived = model,
                ServerNow = DateTime.Now,
                ServerUtcNow = DateTime.UtcNow
            });
        }
    }
}
