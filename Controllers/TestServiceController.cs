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
        public IActionResult TestDateHandling(TestDateModel model, string timeZone)
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
        public IActionResult TestDateHandlingNoFilter(TestDateModel model)
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
