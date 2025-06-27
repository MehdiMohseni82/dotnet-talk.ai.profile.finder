using DotNetTalk.AI.Profile.Finder.ML;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTalk.AI.Profile.Finder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController(IEmbeddingService embeddingService, ILogger<PeopleController> logger) : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger = logger;

        [HttpGet]
        public IActionResult Get()
        {
            var aa = embeddingService.GenerateEmbedding("Hello, world!");
            
            return Ok(aa);
        }
    }
}
