using Microsoft.AspNetCore.Mvc;
using PublicHoliday.Filter;

namespace PublicHoliday.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("")]
    public class WelcomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public WelcomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("")]
        public string HomePage()
        {
            return "Welcome Publich Holiday";
        }
    }
}