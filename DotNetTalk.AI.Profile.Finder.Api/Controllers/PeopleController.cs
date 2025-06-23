using Microsoft.AspNetCore.Mvc;

namespace DotNetTalk.AI.Profile.Finder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController(ILogger<PeopleController> logger) : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger = logger;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
