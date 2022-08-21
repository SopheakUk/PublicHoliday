using Microsoft.AspNetCore.Mvc;
using PublicHoliday.Filter;
using PublicHoliday.Model.Response;
using PublicHoliday.Service.Interface;

namespace PublicHoliday.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("[controller]")]
    public class PublicHolidayController : ControllerBase
    {
        private readonly ILogger<PublicHolidayController> _logger;
        private readonly IPublicHolidayService _publicHolidayService;

        public PublicHolidayController(ILogger<PublicHolidayController> logger,
                                       IPublicHolidayService publicHolidayService)
        {
            _logger = logger;
            _publicHolidayService = publicHolidayService;
        }

        [HttpGet()]
        [Route("GetCountryList")]
        public async Task<List<SupportedCountryResponse>> GetCountryList()
        {
            return await _publicHolidayService.GetCountryList();
        }

        [HttpGet()]
        [Route("GetMonthlyHoliday")]
        public async Task<List<MonthlyHolidayResponse>> GetMonthlyHoliday(int year, string country)
        {
            return await _publicHolidayService.GetMonthlyHoliday(year, country);
        }
    }
}