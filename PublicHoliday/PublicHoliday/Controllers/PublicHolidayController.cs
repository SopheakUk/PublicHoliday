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
        [Route("GetGroupMonthlyHoliday")]
        public async Task<List<GroupMonthlyHolidayResponse>> GetGroupMonthlyHoliday(int year, string country)
        {
            return await _publicHolidayService.GetGroupMonthlyHoliday(year, country);
        }

        [HttpGet()]
        [Route("GetMaximumNumberOfFree")]
        public async Task<int> GetMaximumNumberOfFree(int year, string country)
        {
            return await _publicHolidayService.GetMaximumNumberOfFree(year, country);
        }

        [HttpGet()]
        [Route("GetSpecificDayStatus")]
        public async Task<string> GetSpecificDayStatus(DateTime date, string country)
        {
            return await _publicHolidayService.GetSpecificDayStatus(date, country);
        }
    }
}